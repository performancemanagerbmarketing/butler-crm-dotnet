'use strict';
app.controller('SubCategoryCtrl',
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
            $scope.AddSubCategory = function (SubCategory) {
                console.log(SubCategory);
                SubCategory.CategoryId = SubCategory.Category.Id;
                if (SubCategory.Name == null) {
                    toaster.pop('error', "error", "Please Enter SubCategory Name");
                    return;
                }
                if (SubCategory.Cost == null) {
                    toaster.pop('error', "error", "Please Enter SubCategory Cost");
                    return;
                }

                $scope.AjaxPost("/api/SubCategoryApi/AddSubCategory", SubCategory).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Sub Category Added Successfully");
                            $timeout(function () { window.location.href = '/SubCategory'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add SubCategory");
                        }
                    });
            }

            $scope.EditInit = function () {
                $scope.SubCategory = {};
                $scope.GetCategoryDropdown();
                if ($scope.GetUrlParameter("Id") != null) {
                    var Id = $scope.GetUrlParameter("Id");
                     var data = {
                        Id: parseInt(Id)

                    }
                    $scope.Id = data;
                    console.log(data);
                    $scope.AjaxGet("/api/SubCategoryApi/GetSubCategory", data).then(
                        function (response) {
                            if (response.status == 200) {
                                console.log(response)
                                $scope.SubCategory = response.data;
                            }
                            else {
                                toaster.pop('error', "error", "Not  foound")
                            }
                        });
                }
            }
            $scope.DeleteSubCategory = function () {
                
                $scope.AjaxPost("/api/SubCategoryApi/DeleteSubCategory", { Id: $scope.Id.Id }).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "SubCategory Deleted Successfully!");
                            $timeout(function () { window.location.href = '/SubCategory/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Deleted SubCategory");
                        }
                    });
            }
            $scope.EditSubCategory = function (SubCategory) {
                console.log(SubCategory);
                SubCategory.Id = $scope.Id.Id;
                SubCategory.CategoryId = SubCategory.Category.Id;
                $scope.AjaxPost("/api/SubCategoryApi/EditSubCategory", SubCategory).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "SubCategory Update Successfully!");
                            $timeout(function () { window.location.href = '/SubCategory/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update SubCategory");
                        }
                    });
            }
            $scope.UploadSubCategoryImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.SubCategory[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }

        }]);