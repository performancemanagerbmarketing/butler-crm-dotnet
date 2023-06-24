app.controller('UserCtrl',
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
        function ($scope, $rootScope, $timeout, $q, $window, Upload, $http, toaster, moment) {
            console.log("Connected to Admin Ctrl");
            $scope.Init = function () {

            }

            $scope.AddInit = function () {
                $scope.Admin = {};

            }
            $scope.AddAdmin = function (Admin) {
                console.log(Admin);
                $scope.AjaxPost("/api/AdminApi/RegisterAdmin", Admin).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success && response.data.Success) {
                            toaster.pop('success', "success", "Admin Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/Admin/'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Admin!");
                        }
                    });

             
            }
            $scope.EditInit = function () {
                $scope.Admin = {}

                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }

                console.log(data);
                $scope.AjaxGet("/api/AdminApi/GetAdmin", data).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            console.log(response)
                            $scope.Admin = response.data;
                            $scope.ApprovalStatus
                            if ($scope.Admin.ApprovalStatus == false) {
                                $scope.Admin.ApprovalStatusString = "false";
                            }
                            if ($scope.Admin.ApprovalStatus == true) {
                                $scope.Admin.ApprovalStatusString = "true";
                            }
                        }
                        else {
                            toaster.pop('error', "error", "Not  foound")
                        }
                    });
            }
            $scope.EditAdmin = function (Admin) {
                console.log(Admin);
                if (Admin.ApprovalStatusString == "false") {
                    Admin.ApprovalStatus = false;
                }
                if (Admin.ApprovalStatusString == "true") {
                    Admin.ApprovalStatus = true;
                }

                $scope.AjaxPost("/api/AdminApi/EditAdmin", Admin).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Admin Update Successfully!");
                            $timeout(function () { window.location.href = '/Admin/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Admin");
                        }
                    });
            }
            $scope.UploadAdminImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Admin[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }
        }
    ]);