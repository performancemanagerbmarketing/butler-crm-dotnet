app.controller('ControllerCtrl',
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
            console.log("Connected to Controller Ctrl");
            $scope.Init = function () {

            }

            $scope.AddInit = function () {
                $scope.Controller = {};
                $scope.GetReferenceDropdown();
                $scope.GetCategoryDropdown();
                $scope.GetControlCenterDropdown();
                $scope.GetUploadFiles();
            }
            $scope.AddController = function (Controller) {
                
                var Data = {
                    FirstName: Controller.FirstName,
                    LastName: Controller.LastName,
                    CNIC: Controller.CNIC,
                    ProfileImageUrl: Controller.ProfileImageUrl,
                    CNICFrontImageUrl: Controller.CNICFrontImageUrl,
                    CNICBackImageUrl: Controller.CNICBackImageUrl,
                    Contact: Controller.Contact,
                    Address: Controller.Address,
                    ApprovalStatus: Controller.ApprovalStatus,
                    IsActive: Controller.IsActive,
                    ReferenceId: Controller.Reference.Id,
                    Email: Controller.Email,
                    Password: Controller.Password,
                    ConfirmPassword: Controller.ConfirmPassword,
                    ControlCenterId: Controller.ControlCenter.Id,
                }
                $scope.AjaxPost("/api/ControllerApi/RegisterController", Data).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Controller Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/Controller/'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Controller!");
                        }
                    });


            }
            $scope.EditInit = function () {
                $scope.IsEditable = true;
                $scope.Controller = {}
                $scope.Files = [];
                $scope.GetReferenceDropdown();
                $scope.GetControlCenterDropdown();
                $scope.GetCategoryDropdown();
                var editale = $scope.GetUrlParameter("e");
                if (editale == 'f')
                    $scope.IsEditable = false;
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                $scope.Id = data.Id;
                $scope.GetUploadFiles();
                console.log(data);
                $scope.AjaxGet("/api/ControllerApi/GetController", data).then(
                    function (response) {                        
                        if (response.status == 200 && response.data.Success) {
                            console.log(response)
                            $scope.Controller = response.data;
                            $scope.ApprovalStatus
                            if ($scope.Controller.ApprovalStatus == false) {
                                $scope.Controller.ApprovalStatusString = "false";
                            }
                            if ($scope.Controller.ApprovalStatus == true) {
                                $scope.Controller.ApprovalStatusString = "true";
                            }
                        }
                        else {
                            toaster.pop('error', "error", "Not  foound")
                        }
                    });
                $scope.Controller.Reference = $scope.Controller.ReferenceId
            }
            $scope.EditController = function (Controller) {
                console.log($scope.Id);
                if (Controller.ApprovalStatusString == "false") {
                    Controller.ApprovalStatus = false;
                }
                if (Controller.ApprovalStatusString == "true") {
                    Controller.ApprovalStatus = true;
                }
                var Data = {
                    Id: $scope.Id,
                    FirstName: Controller.FirstName,
                    LastName: Controller.LastName,
                    CNIC: Controller.CNIC,
                    Category: Controller.Category,
                    ProfileImageUrl: Controller.ProfileImageUrl,
                    CNICFrontImageUrl: Controller.CNICFrontImageUrl,
                    CNICBackImageUrl: Controller.CNICBackImageUrl,
                    Contact: Controller.Contact,
                    Address: Controller.Address,
                    ApprovalStatus: Controller.ApprovalStatus,
                    IsActive: Controller.IsActive,
                    ReferenceId: Controller.Reference.Id,
                    ControlCenterId: Controller.ControlCenter.Id,
                }
             
         
                $scope.AjaxPost("/api/ControllerApi/EditController", Data).then(
                    function (response) {
                        console.log(Data);
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Controller Update Successfully!");
                            $timeout(function () { window.location.href = '/Controller/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Controller");
                        }
                    });
            }
            $scope.UploadControllerImage = function (files, prop) {
                console.log(files);
                console.log("prop ", prop);
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Controller[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }

            $scope.UploadFiles = function (files, prop) {
                
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Controller[prop] = response.data.Urls[0];
                        if (response.data.Success) {
                            var File = {
                                Type: files[0].type,
                                FileUploadUrl: response.data.Urls[0],
                                UserId: $scope.Id
                            }
                            $scope.AjaxPost("/api/FileUploadApi/UploadFiles", File).then(
                                function (response) {
                                    if (response.status == 200 && response.data.Success) {
                                        $scope.GetUploadFiles();
                                    }
                                    else {
                                        
                                    }
                                });
                        } else {

                        }
                });
            }

            $scope.GetUploadFiles = function () {
               
                $scope.AjaxGet("/api/FileUploadApi/GetFiles", {UserId: $scope.Id}).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            console.log(response.data.FileUpload);

                            $scope.Files = response.data.FileUpload;
                            console.log($scope.Files)
                        }
                        else {
                            
                        }
                    });
            }

        }
    ]);