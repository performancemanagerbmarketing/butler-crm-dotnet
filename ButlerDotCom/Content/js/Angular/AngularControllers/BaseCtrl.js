

angular.module('main', ['toaster'])

'use strict';
app.controller('baseCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "Upload",
        "$http",
        "toaster",
        "moment",
        function ($scope, $rootScope, $timeout, $q, $window, Upload, $http, toaster,moment) {
            $scope.GetUrlParameter = function (param) {
                const queryString = window.location.search;
                const urlParams = new URLSearchParams(queryString);
                return urlParams.get(param);
            }
            
            //Url Parameter End
            //AjaxServices Start
            $scope.Notifications = [];
            $scope.IsServiceRunning = false;
            $scope.ServiceClassBinder = "LoaderDeActivate"; // bydefualt loader is deactivated
            $scope.TotalNumberOfServicesRunning = 0;
            $scope.AjaxGet = function (link, data) {
                $scope.ServiceClassBinder = "LoaderActivate"; // Loader class Activated
                $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning + 1;
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        if (response.data.Success) {

                        } if (!response.data.Success) {
                            angular.forEach(response.data.ValidationErrors, function (value, key) {
                                //alert(value);
                            }); 
                        }
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated

                    }, function (resp) {
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                    }
                );
                return promise;
            }
            $scope.AjaxGetBackground = function (link, data) {
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        //success

                    }, function (resp) {
                        //error
                    }
                );
                return promise;
            }
            $scope.AjaxPost = function (link, data) {
                $scope.ServiceClassBinder = "LoaderActivate"; // Loader class Activated
                $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning + 1;
                var promise = $http.post(link, data, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {

                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                        if (!response.data.Success) {
                            angular.forEach(response.data.ValidationErrors, function (value, key) {
                                toaster.pop('error', "error", value);
                            });
                        }
                    }, function (resp) {
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                    }
                );
                return promise;
            }
            $scope.ListingOptions = {
                CurrentPage: 1,
                PageSize: 100,
                TotalRecords: 20,
                Url: ''
            }

            //test
            $scope.Pop = function () {
                toaster.pop('success', "success", "text");
            }

            $scope.CloseModel = function (id) {
                $(id).modal("hide");
            }

            $scope.Export = function () {
                var wb = XLSX.utils.table_to_book(document.getElementById('mytable'));
                var wbout = XLSX.write(wb, { bookType: 'xlsx', bookSST: true, type: 'binary' });
                saveAs(new Blob([s2ab(wbout)],
                    { type: "application/octet-stream" }), 'test.xlsx');
            }
            function s2ab(s) {
                var buf = new ArrayBuffer(s.length);
                var view = new Uint8Array(buf);
                for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;
                return buf;
            }


            //sorting and search
            $scope.Sorting = {
                IsAscending : true,
                Field: "",
                AscClass: "active-sort",
                DescClass:"inactive-sort"
            };
            $scope.SetSortingColoumn = function (Field) {
                $scope.Sorting.Field = Field;
                $scope.Sorting.IsAscending = !$scope.Sorting.IsAscending;
                if ($scope.Sorting.IsAscending) {
                    $scope.Sorting.AscClass = "active-sort";
                    $scope.Sorting.DescClass = "inactive-sort";
                }
                if (!$scope.Sorting.IsAscending) {
                    $scope.Sorting.AscClass = "inactive-sort";
                    $scope.Sorting.DescClass = "active-sort";
                }
            }

            
            //chart.js

          
            $scope.GetDatePostFormat = function (date) {
                return moment(date).format("YYYY-MMM-DD")
            }
            $scope.GetDateTimePostFormat = function (date) {
                return moment(date).format("YYYY-MMM-DD hh:mm a")
            }

            $scope.Export = function (fileName) {
                var wb = XLSX.utils.table_to_book(document.getElementById('dataTable'));
                var sheet = wb.Sheets.Sheet1;
                var wscols = [{ wch: 10 }, { wch: 15 }, { wch: 12 }, { wch: 12 }, { wch: 12 }, { wch: 12 }, { wch: 12 }, { wch: 12 }]
                sheet["!cols"] = wscols;
                var wbout = XLSX.write(wb, { bookType: 'xlsx', bookSST: true, type: 'binary' });
                saveAs(new Blob([s2ab(wbout)],
                    { type: "application/octet-stream" }), fileName + '.xlsx');
            }
            function s2ab(s) {
                var buf = new ArrayBuffer(s.length);
                var view = new Uint8Array(buf);
                for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;
                return buf;
            }

            //Images...
            $scope.GetSingleImageUploadUrl = function (files) {
                $scope.SelectedFiles = files;
                if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                    var promise = Upload.upload({
                        url: '/FileUpload/UploadImages',
                        data: {
                            files: $scope.SelectedFiles
                        }
                    });
                    promise.then(function (response) {
                        $timeout(function () {
                            $scope.result = response.data;

                        })
                    }, function (response) {
                        if (response.status > 0) {
                            var errormessage = response.status + ":";

                        }
                    })

                }
                return promise;
            }

            //Current User Login 
            
            $scope.GetCurrentUser = function () {
                $scope.AjaxGet("/api/CurrentLoginUserApi/CurrentLoginUser", null).then(
                    function (response) {
                        $scope.CurrentUser = response.data;
                    });
            }
            $scope.GetNotification = function () {
                $scope.AjaxGetBackground("/api/NotificationApi/GetListing", null).then(
                    function (response) {
                        $scope.Notifications = response.data.Data;
                       
                      if ($scope.TotalNotification < $scope.Notifications.length ) {
                          toaster.pop('success', "success", "You have a new Notification!");
                          $scope.TotalNotification = $scope.Notifications.length;
                        }
                    });
               
            }
            $scope.IsRead = function (Noti) {
                $scope.AjaxPost("/api/NotificationApi/IsRead", { Id: Noti.Id }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success == true) {
                            $timeout(function () { window.location.href = Noti.Link; }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Notification is not read");
                        }
                    });
            }
            $scope.InitNav = function () {
                $scope.CurrentUser = {};
                $scope.Notifications = {};
                $scope.GetNotification();

                $scope.AjaxGet("/api/NotificationApi/GetListing", null).then(
                    function (response) {
                        $scope.Notifications = response.data.Data;
                            $scope.TotalNotification = $scope.Notifications.length;
                });

                //setInterval(function () {
                //    $scope.GetNotification();
                  
                //}, 10000);
                $scope.GetCurrentUser();
               
            }
            //Dropdown Requests.
            $scope.JobStatus = [{ Id: 0, Name: "Pending" }, { Id: 1, Name: "Processing" }, { Id: 2, Name: "Queued" }, { Id: 3, Name: "In Progress" }, { Id: 4, Name: "Complete" }, { Id: 5, Name: "Cancelled" }];
            $scope.PaymentStatus = [{ Id: 0, Name: "Pending" }, { Id: 1, Name: "Done" }, { Id: 2, Name: "Declined" }, { Id: 3, Name: "Discounted" }];
            $scope.GetControlCenterDropdown = function () {
                $scope.AjaxGet("/api/ControlCenterApi/GetControlCenterDropdown", null).then(
                    function (response) {
                        $scope.ControlCenterDropdownList = response.data.Data;
                    });
            }

            $scope.GetCategoryDropdown = function () {
                $scope.AjaxGet("/api/CategoryApi/GetCategoryDropDown", null).then(
                    function (response) {
                        $scope.CategoryDropdownList = response.data.Data;
                    });
            }

            $scope.GetCustomerDropdown = function () {
                $scope.AjaxGet("/api/CustomerApi/CustomerDropDown", null).then(
                    function (response) {
                        $scope.CustomerDropdownList = response.data.Data;
                    });
            }

            $scope.GetReferenceDropdown = function () {
                $scope.AjaxGet("/api/ReferenceApi/GetReferenceDropdown", null).then(
                    function (response) {
                        $scope.ReferenceDropdownList = response.data.Data;
                    });
            }
            $scope.GetSubCategoryDropdown = function () {
                $scope.AjaxGet("/api/SubCategoryApi/GetSubCategoryDropdown", null).then(
                    function (response) {
                        $scope.SubCategoryDropdownList = response.data.Data;
                    });
            }
            $scope.GetWorkerDropdown = function (ControlCenterId) {
                $scope.AjaxGet("/api/WorkerApi/GetWorkerDropdown", { ControllCenterId: ControlCenterId }).then(
                    function (response) {
                        $scope.WorkerDropdownList = response.data.Data;
                    });
            }
            $scope.CalculateTotal = function (array, variable) {
                var total = 0;
                if (array != null) {
                    angular.forEach(array, function (value, key) {
                        if (value[variable] != null)
                            total += value[variable];
                    });
                } 
               
                return total;
            }
            
     }
        ]);