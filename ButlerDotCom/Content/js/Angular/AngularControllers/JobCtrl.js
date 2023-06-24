
app.controller('JobCtrl',
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
        "$filter",
        function ($scope, $rootScope, $timeout, $q, $window, Upload, $http, toaster, moment, $filter) {
            console.log("Connected to Job Ctrl");
            $scope.Init = function () {
                $scope.AddInvoiceDetailRow = function () {
                   
                }
            }
            $scope.AddCustomer = function (Customer) {
                $scope.Job.Customer = {};
                if (Customer == null) {
                    toaster.pop('error', "error", "Enter Name and Number Of Customer");
                    return;
                }
                console.log(Customer.Contact.length);
                if (Customer.FullName == null || Customer.FullName == '') {
                    toaster.pop('error', "error", "Enter Name Of Customer");
                    return;
                }
                if (Customer.Contact == null || Customer.Contact == '' || Customer.Contact.length != 11) {
                    toaster.pop('error', "error", "Enter 11 digit Contact Number of Customer");
                    return;
                }
                $scope.AjaxPost("/api/CustomerApi/AddNewCustomer", Customer).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success == true) {
                            console.log(response);
                            $scope.Job.Customer = { Id: response.data.Id, FullName: response.data.FullName };
                            $scope.CloseModel("#AddCustomer");
                            $scope.GetCustomerDropdown();
                        } else {
                            toaster.pop('error', "error", "Could not add Customer!");
                        }
                    });
            }
           
            $scope.AddRow = function (Worker) {
                console.log(Worker);
                var row = {
                    WorkerId:Worker.Id,
                    WorkerName: Worker.Name,
                    CNIC: Worker.CNIC,
                    Contact: Worker.Contact,
                    JobId: $scope.Id.Id
                };
                if ($scope.Job.JobWorker == null) {
                    $scope.Job.JobWorker = [];
                }
                $scope.Job.JobWorker.push(row);
                $scope.Job.Worker = "";
            }
            $scope.TotalAmount = 0;
            $scope.AddRowSub = function (SubCategory) {
                var row = {
                    SubCategoryId: SubCategory.Id,
                    SubCategoryName: SubCategory.Name,
                    Amount: SubCategory.Amount,
                    Discount: "",
                    JobId: $scope.Id.Id,
                };
                if ($scope.Job.JobDetail == null) {
                    $scope.Job.JobDetail = [];
                }
                $scope.Job.JobDetail.push(row);
                $scope.TotalAmount = $scope.TotalAmount + SubCategory.Amount;
            }
            $scope.GetTotal = function () {
                return 200;
            }
            $scope.DeleteWorker = function (index) {
                $scope.Job.JobWorker.splice(index, 1);                
            }
            $scope.DeleteSub = function (index,Amount) {
                $scope.Job.JobDetail.splice(index, 1);
                $scope.UpdateAmount();
            }

            $scope.DeleteOther = function (index) {
                $scope.Job.JobDetail.splice(index, 1);
                $scope.UpdateAmount();
            }

            $scope.AddOtherRow = function () {
                console.log("Other")
                var row = {
                    OthersName:"",
                    Cost: 0,
                    JobId: $scope.Id.Id
                };
                if ($scope.Job.Others == null) {
                    $scope.Job.Others = [];
                }
                $scope.Job.Others.push(row);
            }

            $scope.AddMaterialRow = function () {
                var row = {
                    MaterialName: "",
                    Cost: 0,
                };
                if ($scope.Job.Material == null) {
                    $scope.Job.Material = [];
                }
                $scope.Job.Material.push(row);
            }
            $scope.UpdateTotalAmount = function () {
                console.log("Total", $scope.TotalAmount);
                $scope.MaterialAmount = 0;
                angular.forEach($scope.Job.Material, function (value, key) {
                    $scope.MaterialAmount += value.Cost;
                })
                
            }
            $scope.DeleteMaterial = function (index, Amount) {
                $scope.Job.Material.splice(index, 1);
                $scope.MaterialAmount = $scope.MaterialAmount - Amount;
            }
            $scope.AddMaterialCost = function (Material) {
                console.log(Material);
                if (Material == null) {
                    toaster.pop('error', "error", "Please add material or Add only two to three Material Item");
                    return;
                }
                $scope.AjaxPost("/api/JobApi/AddJobDetail", { JobId: $scope.Id.Id, Material: $scope.Job.Material }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Job Material Added Successfully!");
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add Job!");
                        }
                    });

            }

            $scope.AddOtherService = function (Other) {
                if (Other == null) {
                    toaster.pop('error', "error", "Please add other service or Add only two to three Material Item");
                    return;
                }
                $scope.AjaxPost("/api/JobApi/AddOtherService", { JobId: $scope.Id.Id, Others: $scope.Job.Others }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Job Others Added Successfully!");
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not update Job!");
                        }
                    });

            }

            $scope.ApplyDiscount = function (Job) {
                console.log("Job", Job);
                if (Job.Discount == null || Job.Discount == 0) {
                    toaster.pop('error', "error", "Discount can not be null!");
                    return;
                }
                $scope.AjaxPost("/api/JobApi/ApplyDiscount", { Id: Job.Id, Discount: Job.Discount }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Discount apply successfully!");
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not update Job!");
                        }
                    });
            }

            $scope.UpdateStatus = function (Status) {
                console.log(Status);
                var Data = {
                    JobId: $scope.Id.Id,
                    Status: Status
                }
                $scope.AjaxPost("/api/JobApi/UpdateStatus", Data).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            console.log(response);
                            toaster.pop('success', "success", "Job Status Updated Successfully!");
                            //var url = "https://pk.eocean.us/APIManagement/API/RequestAPI?user=butler1&pwd=ALXcds45iTEQ8Jtj4aLa%2fVhxVQeCnH8NgN1RgyXsn%2bXAGHq58R9iWV9z8FFKwv9noQ%3d%3d&sender=BUTLER&reciever=" + response.data.Contact + "&msg-data=" + response.data.Title + "&response=string";
                            //$scope.AjaxGet(url, null).then(
                            //    function (response) {
                            //        toaster.pop('success', "success", "Job Status Updated Successfully!");
                            //    });
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Job!");
                        }
                    });
            }

            $scope.UpdatePaymentStatus = function () {
                console.log("fdfsdfdsfsdf");
             
                $scope.AjaxPost("/api/JobApi/AmountPaid", { JobId: $scope.Id.Id}).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Job Added Successfully!");
                            console.log(response);

                            var url = "https://pk.eocean.us/APIManagement/API/RequestAPI?user=butler1&pwd=ALXcds45iTEQ8Jtj4aLa%2fVhxVQeCnH8NgN1RgyXsn%2bXAGHq58R9iWV9z8FFKwv9noQ%3d%3d&sender=BUTLER&reciever=" + response.data.Contact + "&msg-data=" + response.data.Title + "&response=string";
                            $scope.AjaxGet(url, null).then(
                                function (response) {
                                });
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Job!");
                        }

                    });
            }
            $scope.GetDateTimePostFormat = function (date) {
                return moment(date).format("YYYY-MMM-DD hh:mm a")
            }
            $scope.AddInit = function () {
                $scope.Job = {};
                $scope.GetCategoryDropdown();
                $scope.GetCustomerDropdown();
                $scope.GetControlCenterDropdown();
                $scope.Job.BookingDate = new Date(moment().format("YYYY-MMM-DD hh:mm a"));
                console.log($scope.Job.BookingDate)
            }
            $scope.AddJob = function (Job) {
                if (Job.Title == null) {
                    toaster.pop('error', "error", "Please Enter Title!");
                    return;
                }
                if (Job.Description == null) {
                    toaster.pop('error', "error", "Please Enter Description!");
                    return;
                }
                if (Job.JobAddress == null) {
                    toaster.pop('error', "error", "Please Enter Job Address!");
                    return;
                }
                if (Job.Category == null) {
                    toaster.pop('error', "error", "Please Select Category!");
                    return;
                }
                if (Job.Customer == null) {
                    toaster.pop('error', "error", "Please Select Customer!");
                    return;
                }
                if (Job.BookingDate == null || Job.BookingDate == '') {
                    toaster.pop('error', "error", "Please Select Correct Date Time!");
                    return;
                }
                Job.BookingDate = moment(Job.BookingDate).format("DD-MMM-YYYY hh:mm a");
                console.log(Job);
                
                $scope.AjaxPost("/api/JobApi/AddJob", Job).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success ) {
                            toaster.pop('success', "success", "Job Added Successfully!");
                            $timeout(function () { window.location.href = '/Job/Index'; }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Job!");
                        }
                });
            }
            $scope.EditInit = function () {
                $scope.Job = {};
                $scope.Job.JobWorker = [];
                $scope.Job.Material = [];
                $scope.Job.Others = [];
                $scope.GetCategoryDropdown();
                $scope.GetCustomerDropdown();
                $scope.GetControlCenterDropdown();
                $scope.GetSubCategoryDropdown();
                $scope.SubAmount = 0
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                $scope.Id = data;
                console.log(data);
                $scope.AjaxGet("/api/JobApi/GetJob", data).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            console.log(response)
                            $scope.Job = response.data;
                            console.log($scope.Job.Others)
                            $scope.TotalAmount = $scope.Job.TotalAmount;
                            $scope.MaterialAmount = $scope.Job.TotalAmount;
                            $scope.Job.BookingDate = new Date($scope.Job.BookingDate);
                            $scope.GetWorkerDropdown($scope.Job.ControlCenter.Id);
                        }
                        else {
                            toaster.pop('error', "error", "Not  foound")
                        }
                    });
            }
            $scope.EditJob = function (Job) {

                $scope.AjaxPost("/api/JobApi/EditJob", Job).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Job Update Successfully!");
                            $timeout(function () { window.location.href = '/Job/Index' }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Update Job");
                        }
                    });
            }
            $scope.silentDetailInit = function () {
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                $scope.Id = data;
                $scope.AjaxGetBackground("/api/JobApi/GetJob", data).then(
                    function (response) {

                        if (response.status == 200) {
                            console.log(response)
                            $scope.Job = response.data;
                            $scope.TotalAmount = $scope.Job.TotalAmount;
                        }
                        else {
                            toaster.pop('error', "error", "Unable To Retrieve Data!")
                        }
                    });
            }
            $scope.UploadJobImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);

                        $scope.Job[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    });
            }
            $scope.AddWorker = function () {
                if ($scope.Job.JobWorker.length == 0) {
                    toaster.pop('error', "error", "Please Add Workers!");
                    return;
                }
                $scope.AjaxPost("/api/JobApi/AddJobWorker", { JobId: $scope.Id.Id, JobWorker: $scope.Job.JobWorker }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Job Worker Added Successfully!");
                            angular.forEach($scope.Job.JobWorker, function (value, key) {
                                var content = "New Job Assigned! Job Id # " + $scope.Job;
                                $scope.SendMessage(value.Contact, content);
                            });
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Job!");
                        }
                    });
            }
            $scope.SendMessage = function (number,content) {
                //var url = "https://pk.eocean.us/APIManagement/API/RequestAPI?user=butler1&pwd=ALXcds45iTEQ8Jtj4aLa%2fVhxVQeCnH8NgN1RgyXsn%2bXAGHq58R9iWV9z8FFKwv9noQ%3d%3d&sender=BUTLER&reciever=" + number + "&msg-data=" + content + "&response=string";
                //$scope.AjaxGetBackground(url, null).then(
                //    function (response) {

                //    });
            }
            $scope.AddSubCategory = function () {
                if ($scope.Job.JobDetail.length == 0) {
                    toaster.pop('error', "error", "Please Add SubCategory!");
                    return;
                }
                $scope.AjaxPost("/api/JobApi/AddJobDetail", { JobId: $scope.Id.Id, JobDetail: $scope.Job.JobDetail }).then(
                    function (response) {
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Job Worker Added Successfully!");
                            $timeout(function () { $scope.silentDetailInit(); }, 2000);
                        } else {

                            toaster.pop('error', "error", "Could not add Job!");
                        }
                    });
            }
            
            $scope.UpdateAmount = function () {
                console.log($scope.Job);
                var Total = 0;
                angular.forEach($scope.Job.JobDetail, function (value, key) {
                    Total += value.Amount;
                })
                $scope.SubAmount = Total;
            }
        }
    ]);