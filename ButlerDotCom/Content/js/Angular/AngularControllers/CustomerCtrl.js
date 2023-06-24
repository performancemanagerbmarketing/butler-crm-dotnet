app.controller('CustomerCtrl',
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
            console.log("Connected to Customer Ctrl");
            $scope.Init = function () {

            }

            $scope.AddInit = function () {
                $scope.Customer = {};

            }
            $scope.AddCustomer = function (Customer) {

                $scope.AjaxPost("/api/CustomerApi/AddCustomer", Customer).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Customer Added Successfully!");
                            $timeout(function () { window.location.href = '/Customer/Index'; }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Customer!");
                        }
                    });
            }
            $scope.EditInit = function () {
                $scope.Customer = {}

                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }

                console.log(data);
                $scope.AjaxGet("/api/CustomerApi/GetCustomer", data).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response)
                            $scope.Customer = response.data;
                        }
                        else {
                            toaster.pop('error', "error", "Not  foound")
                        }
                    });
            }
            $scope.EditCustomer = function (Customer) {

                $scope.AjaxPost("/api/CustomerApi/EditCustomer", Customer).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Customer Update Successfully!");
                            $timeout(function () { window.location.href = '/Customer/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Customer");
                        }
                    });
            }
            $scope.UploadCustomerImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Customer[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }
        }
    ]);