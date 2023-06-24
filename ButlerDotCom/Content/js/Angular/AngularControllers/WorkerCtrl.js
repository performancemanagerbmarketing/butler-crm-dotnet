app.controller('WorkerCtrl',
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
            console.log("Connected to Worker Ctrl");
            $scope.Init = function () {

            }


            $scope.AddInit = function () {
                $scope.Worker = {};
                $scope.GetControlCenterDropdown();
            }
            $scope.AddWorker = function (Worker) {
                if (Worker.FirstName == null || Worker.FirstName == '') {
                    toaster.pop('error', "error", "Please enter First Name");
                    return;
                }
                if (Worker.LastName == null || Worker.LastName == '') {
                    toaster.pop('error', "error", "Please enter First Name");
                    return;
                }

                if (Worker.CNIC == null || Worker.CNIC == '') {
                    toaster.pop('error', "error", "Please enter CNIC Number");
                    return;
                }
                if (Worker.Contact == null || Worker.Contact == '') {
                    toaster.pop('error', "error", "Please enter Contact Number");
                    return;
                }
                if (Worker.Email == null || Worker.Email == '') {
                    toaster.pop('error', "error", "Please enter email");
                    return;
                }
                if (Worker.Password == null || Worker.Password == '') {
                    toaster.pop('error', "error", "Please enter password");
                    return;
                }
                if (Worker.ConfirmPassword == null || Worker.ConfirmPassword == '') {
                    toaster.pop('error', "error", "Please enter Confirm Password");
                    return;
                }
                if (Worker.Password != Worker.ConfirmPassword) {
                    toaster.pop('error', "error", "Password & Confirm Password does not match.");
                    return;
                }
                if (Worker.UserName == null || Worker.UserName == '') {
                    toaster.pop('error', "error", "Please enter username");
                    return;
                }
                if (Worker.ControlCenter == null) {
                    toaster.pop('error', "error", "Please select Control Center");
                    return;
                }
                var Data = {
                    FirstName: Worker.FirstName,
                    LastName: Worker.LastName,
                    CNIC: Worker.CNIC,
                    Contact: Worker.Contact,
                    Address: Worker.Address,
                    Email: Worker.Email,
                    UserName: Worker.UserName,
                    Password: Worker.Password,
                    IsActive: Worker.IsActive,
                    ConfirmPassword: Worker.ConfirmPassword,
                    CNICBackImageUrl: Worker.CNICBackImageUrl,
                    CNICFrontImageUrl: Worker.CNICFrontImageUrl,
                    ProfileImageUrl: Worker.ProfileImageUrl,
                    ControlCenterId: Worker.ControlCenter.Id,
                }

                $scope.AjaxPost("/api/WorkerApi/RegisterWorker", Data
                ).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Worker Added Successfully!");
                            $timeout(function () { window.location.href = '/Worker/Index'; }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Worker!");
                        }
                    });
            }
            $scope.EditInit = function () {
                $scope.Worker = {}
                $scope.GetControlCenterDropdown();
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                $scope.Id = data;

                console.log($scope.Id);
                $scope.AjaxGet("/api/WorkerApi/GetWorker", data).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            console.log(response)
                            $scope.Worker = response.data;
                        }
                        else {
                            toaster.pop('error', "error", "Not  foound")
                        }
                    });
                $scope.ControlCenterId = Worker.ControlCenterId;
            }
            $scope.EditWorker = function (Worker) {
                if (Worker.FirstName == null || Worker.FirstName == '') {
                    toaster.pop('error', "error", "Please enter First Name");
                    return;
                }
                if (Worker.LastName == null || Worker.LastName == '') {
                    toaster.pop('error', "error", "Please enter First Name");
                    return;
                }

                if (Worker.CNIC == null || Worker.CNIC == '') {
                    toaster.pop('error', "error", "Please enter CNIC Number");
                    return;
                }
                if (Worker.Contact == null || Worker.Contact == '') {
                    toaster.pop('error', "error", "Please enter Contact Number");
                    return;
                }
                if (Worker.ControlCenter == null) {
                    toaster.pop('error', "error", "Please select Control Center");
                    return;
                }
                console.log(Worker);
                
                $scope.AjaxPost("/api/WorkerApi/EditWorker", { Worker: Worker }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Worker Update Successfully!");
                            $timeout(function () { window.location.href = '/Worker/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Worker");
                        }
                    });
            }
            $scope.UploadWorkerImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Worker[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }
        }
    ]);