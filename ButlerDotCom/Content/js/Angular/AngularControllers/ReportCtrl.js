app.controller('ReportCtrl',
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
            console.log("Connected to Reference Ctrl");
            $scope.Init = function () {

            }

            //$scope.AddInit = function () {
            //    $scope.JobReport = {};

            //}
            //$scope.AddReference = function (Reference) {

            //    $scope.AjaxPost("/api/ReportApi/GetJobReport", Reference).then(
            //        function (response) {
            //            if (response.status == 200) {
            //                toaster.pop('success', "success", "Reference Added Successfully!");
            //                $timeout(function () { window.location.href = '/Reference/Index'; }, 2000);
            //            } else {

            //                toaster.pop('error', "error", "Could not add Reference!");
            //            }
            //        });
            //}
         
        }
    ]);