'use strict';
app.controller('ControlCenterCtrl',
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
                $scope.ControlCenter = {};
            }
            $scope.AddControlCenter = function (ControlCenter) {
                console.log(ControlCenter);
                $scope.AjaxPost("/api/ControlCenterApi/AddControlCenter", ControlCenter).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "text");
                            $timeout(function () { window.location.href = '/ControlCenter'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add ControlCenter");
                        }
                    });
            }

            $scope.EditInit = function () {
                $scope.ControlCenter = {};
                if ($scope.GetUrlParameter("Id") != null) {
                    var Id = $scope.GetUrlParameter("Id");
                    var data = {
                        Id: parseInt(Id)

                    }
                    console.log(data);
                    $scope.AjaxGet("/api/ControlCenterApi/GetControlCenter", data).then(
                        function (response) {
                            if (response.status == 200) {
                                console.log(response)
                                $scope.ControlCenter = response.data;
                            }
                            else {
                                toaster.pop('error', "error", "Not  foound")
                            }
                        });
                }
            }
            $scope.EditControlCenter = function (ControlCenter) {
                console.log(ControlCenter);
                $scope.AjaxPost("/api/ControlCenterApi/EditControlCenter", ControlCenter).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Security Company Update Successfully!");
                            $timeout(function () { window.location.href = '/ControlCenter/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Security Company");
                        }
                    });
            }

        }]);