'use strict';
app.controller('CategoryCtrl',
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
                $scope.Category = {}
                $scope.GetControlCenterDropdown();
                                
            }
            $scope.AddCategory = function (Category) {
                console.log(Category);
               
                if (Category.Name == null) {
                    toaster.pop('error', "error", "Please Enter Category Name");
                    return;
                }
                if (Category.Description == null) {
                    toaster.pop('error', "error", "Please Enter Category Description");
                    return;
                }
                if (Category.ProfileImageUrl == null) {
                    toaster.pop('error', "error", "Please select ProfileImageUrl.");
                    return;
                }

                $scope.AjaxPost("/api/CategoryApi/AddCategory", Category).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Category Added Successfully!");
                            $timeout(function () { window.location.href = '/Category'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add Category");
                        }
                    });
            }

            $scope.EditInit = function () {
                $scope.Category = {};
                if ($scope.GetUrlParameter("Id") != null) {
                    var Id = $scope.GetUrlParameter("Id");
                    var data = {
                        Id: parseInt(Id)

                    }
                    $scope.Id = data.Id;
                    console.log(data);
                    $scope.AjaxGet("/api/CategoryApi/GetCategory", data).then(
                        function (response) {
                            if (response.status == 200) {
                                console.log(response)
                                $scope.Category = response.data;
                            }
                            else {
                                toaster.pop('error', "error", "Not  foound")
                            }
                        });
                }
            }

            $scope.EditCategory = function (Category) {
                console.log(Category);
                $scope.AjaxPost("/api/CategoryApi/EditCategory", Category).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Category Update Successfully!");
                            $timeout(function () { window.location.href = '/Category/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Category");
                        }
                    });
            }

            $scope.DeleteCategory = function () {
                console.log(Category);
                $scope.AjaxPost("/api/CategoryApi/DeleteCategory", {Id: $scope.Id}).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Category Deleted Successfully!");
                            $timeout(function () { window.location.href = '/Category/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Deleted Category");
                        }
                    });
            }
            $scope.UploadCategoryImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Category[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }

        }]);