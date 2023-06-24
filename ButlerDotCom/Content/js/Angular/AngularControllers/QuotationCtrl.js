'use strict';
app.controller('QuotationCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        "moment",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster, moment) {
            $scope.init = function () {

            }
            $scope.AddInit = function () {
                $scope.GetCategoryDropdown();

            }
            $scope.AddQuotation = function (Quotation) {
                console.log(Quotation);

                if (Quotation.Name == null || Quotation.Name == '') {
                    toaster.pop('error', "error", "Please Enter Quotation Name");
                    return;
                }
                if (Quotation.Number == null || Quotation.Name == '') {
                    toaster.pop('error', "error", "Please Enter Quotation Number");
                    return;
                }
                if (Quotation.Service == null || Quotation.Service == '') {
                    toaster.pop('error', "error", "Please Enter Quotation Service");
                    return;
                }

                $scope.AjaxPost("/api/QuotationApi/AddQuotation", { Quotation: Quotation}).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Quotation Added Successfully");
                            $timeout(function () { window.location.href = '/Quotation'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add Quotation");
                        }
                    });
            }

            $scope.EditInit = function () {
                $scope.Quotation = {};
                $scope.GetCategoryDropdown();
                if ($scope.GetUrlParameter("Id") != null) {
                    var Id = $scope.GetUrlParameter("Id");
                    var data = {
                        Id: parseInt(Id)

                    }
                    $scope.Id = data;
                    console.log(data);
                    $scope.AjaxGet("/api/QuotationApi/GetQuotation", data).then(
                        function (response) {
                            if (response.status == 200) {
                                console.log(response)
                                $scope.Quotation = response.data.Quotation;
                            }
                            else {
                                toaster.pop('error', "error", "Not  foound")
                            }
                        });
                }
            }
            
            $scope.EditQuotation = function (Quotation) {
                console.log(Quotation);
                if (Quotation.Name == null || Quotation.Name == '') {
                    toaster.pop('error', "error", "Please Enter Quotation Name");
                    return;
                }
                if (Quotation.Number == null || Quotation.Name == '') {
                    toaster.pop('error', "error", "Please Enter Quotation Number");
                    return;
                }
                if (Quotation.Service == null || Quotation.Service == '') {
                    toaster.pop('error', "error", "Please Enter Quotation Service");
                    return;
                }
                $scope.AjaxPost("/api/QuotationApi/EditQuotation", { Quotation: Quotation }).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Quotation Update Successfully!");
                            $timeout(function () { window.location.href = '/Quotation/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Quotation");
                        }
                    });
            }

        }]);