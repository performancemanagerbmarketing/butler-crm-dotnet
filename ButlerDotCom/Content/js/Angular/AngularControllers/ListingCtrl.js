'use strict';
app.controller('ListingCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "Upload",
        function ($scope, $rootScope, $timeout, $q, $window, $http, Upload) {
            $scope.ListingOptions = {
                CurrentPage: 1,
                PageSize: 100,
                TotalRecords: 20,
                Url: '',
                Filters: {},
                SortBy: "",
            }
            $scope.NextPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage + 1;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.PreviousPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage - 1;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.UpdatePaginationSize = function () {
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.ResetList = function (data) {
                $scope.AjaxPost(data.Url, data).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                    });
            }
            $scope.InitListing = function () {
                $scope.AjaxPost($scope.ListingOptions.Url, $scope.ListingOptions).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                        
                    });
            }
            $scope.ApplyFilters = function (filters) {
                $scope.ListingOptions.Filters = filters;
                console.log($scope.ListingOptions);
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.ResetFilters = function () {
                $scope.Filters = {};
                $scope.ApplyFilters($scope.Filters);
            }
            $scope.SetFilters = function (filter) {
                if (filter.DateFrom != null) {
                    filter.DateFrom = $scope.GetDatePostFormat(filter.DateFrom);
                }
                if (filter.DateTo != null) {
                    filter.DateTo = $scope.GetDatePostFormat(filter.DateTo);
                }
                $scope.ApplyFilters(filter);
            }
            $scope.InitListingWithFilter = function (filter) {
               
                if (filter.DateFrom != null) {
                    filter.DateFrom = $scope.GetDatePostFormat(filter.DateFrom);
                }
                if (filter.DateTo != null) {
                    filter.DateTo = $scope.GetDatePostFormat(filter.DateTo);
                }
                $scope.ListingOptions.Filters = filter;
                $scope.AjaxPost($scope.ListingOptions.Url, $scope.ListingOptions).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                    });
            }
            $scope.ResetFilters = function () {
                $scope.Filters = {};
                $scope.ApplyFilters($scope.Filters);
            }
            $scope.SetFilters = function (filter) {
                if (filter.DateFrom != null) {
                    filter.DateFrom = $scope.GetDatePostFormat(filter.DateFrom);
                }
                if (filter.DateTo != null) {
                    filter.DateTo = $scope.GetDatePostFormat(filter.DateTo);
                }
                if (filter.Date != null) {
                    filter.Date = $scope.GetDatePostFormat(filter.Date);
                }
                $scope.ApplyFilters(filter);
            }

        }]);