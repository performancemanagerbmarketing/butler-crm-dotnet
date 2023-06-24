app.controller('ReferenceCtrl',
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

            $scope.AddInit = function () {
                $scope.Reference = {};
               
            }
            $scope.AddReference = function (Reference) {

                $scope.AjaxPost("/api/ReferenceApi/AddReference", Reference).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Reference Added Successfully!");
                            $timeout(function () { window.location.href = '/Reference/Index'; }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Reference!");
                        }
                    });
            }
          
            $scope.EditReference = function (Reference) {

                $scope.AjaxPost("/api/ReferenceApi/EditReference", Reference).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Reference Update Successfully!");
                            $timeout(function () { window.location.href = '/Reference/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Reference");
                        }
                    });
            }
            $scope.UploadReferenceImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Reference[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }

            $scope.EditInit = function () {
                $scope.Reference = {};
                $scope.IsEditAllowed = true;
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                var e = $scope.GetUrlParameter("e");
                if (e == 'f') {
                    $scope.IsEditAllowed = false;

                }

                console.log(data);
                $scope.AjaxGet("/api/ReferenceApi/GetReference", data).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response)
                            $scope.Reference = response.data;
                        }
                        else {
                            toaster.pop('error', "error", "Not  foound")
                        }
                    });
            }
        }]);