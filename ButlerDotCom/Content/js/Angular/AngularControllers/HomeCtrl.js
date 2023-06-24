'use strict';
app.controller('HomeCtrl',
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
            console.log("Connected to Bulter App Home ctrl");
            $scope.Summary = {};
            $scope.Request = {};
            $scope.InitDashboard = function () {
                $scope.JobSummaryList = {};
                $scope.Request.DateFrom = new Date();
                $scope.Request.DateTo = new Date();
                $scope.GetSummary($scope.Request);
                $scope.Summary.CompletedJobs = 10;
                $scope.Summary.JobsStarted = 10;
                $scope.Summary.AwaitingCompletionJobs = 10;
                $scope.Summary.CancelledJobs = 10;
                $scope.Summary.Customers = 20;
                $scope.Summary.PendingJobs = 30;
                $scope.Summary.Revenue = 100;
                $scope.GetJobSummaryList();

                $scope.labels_SiteFeul = ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JLY', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'];
                $scope.series_SiteFeul = ['Series A', 'Series B'];

                $scope.data_SiteFeul = [
                    [65, 59, 80, 81, 55, 46, 50, 50, 23, 88, 21, 32],
                    [28, 48, 40, 19, 86, 27, 90, 34, 78, 23, 12, 89]
                ];

                $scope.labels1 = ['Plumber', 'AC Technicians', 'Electricians', 'Fumigation', 'Cleaner'];
                $scope.series1 = ['Plumber', 'AC Technicians', 'Electricians', 'Fumigation', 'Cleaner'];

                $scope.data1 = [
                    [65, 59, 80, 81, 56],
                    [28, 48, 40, 19, 86],
                    [58, 48, 40, 19, 86],
                    [68, 48, 40, 19, 86],
                    [18, 48, 40, 19, 86]
                ];

                $scope.labels2 = ['Medical1', 'Medical2', 'Medical4', 'Medical5', 'Medical6'];
                $scope.series2 = ['Medical1', 'Medical2', 'Medical4', 'Medical5', 'Medical6'];

                $scope.data2 = [
                    [65, 59, 80, 82, 56],
                    [28, 48, 40, 29, 86],
                    [58, 48, 40, 29, 86],
                    [68, 48, 40, 29, 86],
                    [28, 48, 40, 29, 86]
                ];
                //$scope.labels3 = ['Plumber', 'AC Technicians', 'Electricians', 'Fumigation', 'Cleaner','Demo']
                //$scope.series3 = ['Hummam ', 'Rameez', 'Azam', 'Asad', 'Orangzaib'];

                $scope.data3 = [
                    [65, 59, 80, 83, 56],
                    [28, 48, 40, 39, 86],
                    [58, 48, 40, 39, 86],
                    [68, 48, 40, 39, 86],
                    [38, 48, 40, 39, 86]
                ];


                $scope.labels_SiteSharing = ['Mobilink', 'Ufone', 'Zong', 'Telenor', 'PTCL'];
                $scope.series_SiteSharing = ['Site Sharing', 'Site Sharing1'];

                $scope.data_SiteSharing = [
                    [95, 79, 65, 51, 44],
                    [95, 79, 65, 51, 41]


                ];

                $scope.Site_Tower_labels = ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'];
                $scope.Site_Tower_series = ['Active', 'Non-Active'];
                //Chart.defaults.global.colors = ['#46BFBD', '#FDB45C'];

                //Tickets Statistics
                $scope.Site_Tower_data = [
                    [3, 2, 1, 0, 3, 3, 4, 3, 2, 0, 5, 1],
                    [5, 6, 4, 6, 2, 7, 6, 6, 4, 5, 7, 8]
                ];

                $scope.Tickets_labels = ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'];
                $scope.Tickets_series = ['Pending', 'Resolved'];
                //Chart.defaults.global.colors = ['#46BFBD', '#FDB45C'];

                $scope.Tickets_data = [
                    [3, 2, 1, 0, 3, 3, 4, 3, 2, 0, 5, 1],
                    [5, 6, 4, 6, 2, 7, 6, 6, 4, 5, 7, 8]
                ];

                //TicketsByDate-Graph
                $scope.TicketsByDate_labels = ["Open", "Resolved", "Pending"];
                $scope.TicketsByDate_data = [9, 7, 2];

                //ONM projects-graph
                $scope.ONM_labels = ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'];
                $scope.ONM_series = ['Approved', 'Pending', "Resolved"];
                $scope.ONM_data = [
                    [3, 2, 1, 0, 3, 3, 4, 3, 2, 0, 5, 1],
                    [5, 6, 4, 6, 2, 7, 6, 6, 4, 5, 7, 8],
                    [7, 9, 10, 8, 5, 2, 8, 12, 9, 5, 12, 6]
                ];

                //Network Implementation projects-graph
                $scope.NI_labels = ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'];
                $scope.NI_series = ['Approved', 'Pending', "Resolved"];
                $scope.NI_data = [
                    [3, 2, 1, 0, 3, 3, 4, 3, 2, 0, 5, 1],
                    [5, 6, 4, 6, 2, 7, 6, 6, 4, 5, 7, 8],
                    [7, 9, 10, 8, 5, 2, 8, 12, 9, 5, 12, 6]
                ];
            }
           
            $scope.GetSummary = function (req) {
                console.log(req);

                $scope.AjaxGet("/api/DashboardApi/GetJobSummaryList", req).then(
                    function (response) {
                        if (response.data.Success) {
                            $scope.JobSummaryList = response.data;
                        }
                    });

                $scope.AjaxGet("/api/DashboardApi/GetDashboardSummary", req).then(
                    function (response) {
                        console.log(response);
                        $scope.Summary = response.data;
                        $scope.data1 = [];
                        $scope.data1.push(response.data.GeneralServicesCompletionChart.Jobs);
                        $scope.labels1 = response.data.GeneralServicesCompletionChart.Categories;
                        $scope.series1 = response.data.GeneralServicesCompletionChart.Categories;

                        $scope.data2 = [];
                        $scope.data2.push(response.data.MedicalServicesCompletionChart.Jobs);
                        $scope.labels2 = response.data.MedicalServicesCompletionChart.Categories;
                        $scope.series2 = response.data.MedicalServicesCompletionChart.Categories;

                        $scope.data3 = [];
                        $scope.data3.push(response.data.Workers.Jobs);
                        $scope.labels3 = response.data.Workers.WorkerName;
                        $scope.series3 = response.data.Workers.Categories;
                        console.log($scope.CustomerDropdownList);
                    });
            }
        }]);