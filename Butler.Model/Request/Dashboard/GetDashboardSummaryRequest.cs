using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Dashboard
{
    public class GetDashboardSummaryResponse : Response
    {
       public int CompletedJobs { get; set; }
        public int Customers { get; set; }
        public int PendingJobs { get; set; }
        public int CancelledJobs { get; set; }
        public int JobsStarted { get; set; }
        public int AwaitingCompletionJobs { get; set; }
        public string TotalAmount { get; set; }
        public decimal Revenue { get; set; }

        public GeneralServicesCompletionChart GeneralServicesCompletionChart { get; set; }
        public GeneralServicesCompletionChart MedicalServicesCompletionChart { get; set; }
        public Workers Workers { get; set; }
    }
    public class GeneralServicesCompletionChart
    {
        public List<string> Categories { get; set; }
        public List<int> Jobs { get; set; }
    }
    public class Workers
    {
        public List<string> WorkerName { get; set; }
        public List<string> Categories { get; set; }
        public List<int> Jobs { get; set; }
    }
    public class GetDashboardSummaryRequest
    {
        public string UserId { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetDashboardSummaryRequest req)
        {
            var response = new GetDashboardSummaryResponse();
            response.GeneralServicesCompletionChart = new GeneralServicesCompletionChart();
            response.MedicalServicesCompletionChart = new GeneralServicesCompletionChart();
            response.Workers = new Workers();
            response.ValidationErrors = new List<string>();
            try
            {
                var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                var Jobs = _dbContext.Job.ToList();
                if (req.DateFrom.HasValue)
                {
                    Jobs = Jobs.Where(x => x.BookingDate >= req.DateFrom).ToList();
                }  
                if (req.DateTo.HasValue)
                {
                    Jobs = Jobs.Where(x => x.BookingDate <= req.DateTo).ToList();
                }
                
                if (Agent.UserType == (int)UserType.Controller)
                {
                    Jobs = Jobs.Where(x => x.ControlCenterId == Agent.ControllerCenterId).ToList();
                }
                response.JobsStarted = Jobs.Where(x => x.Status == (int)JobStatus.In_Progress).Count();
                response.AwaitingCompletionJobs = Jobs.Where(x => x.Status == (int)JobStatus.Processing).Count();
                response.CancelledJobs = Jobs.Where(x => x.Status == (int)JobStatus.Cancelled).Count();
                response.CompletedJobs = Jobs.Where(x => x.Status == (int)JobStatus.Complete).Count();
                response.PendingJobs = Jobs.Where(x => x.Status == (int)JobStatus.Pending).Count();
                response.Revenue = Jobs.Where(x=>x.Status == (int)JobStatus.Complete ).Sum(x => x.TotalAmount) ?? 0;
                response.Customers = _dbContext.UserProfile.Where(x=>x.UserType == (int)UserType.Customer).Count();

                var Categories = _dbContext.Category;
                var GeneralCategories = Categories.Where(x => x.Type == (int)CategoryType.General);
                var MedicalCategories = Categories.Where(x => x.Type == (int)CategoryType.Medical);
                var JobWorkers = _dbContext.JobWorker;
                var Workers = JobWorkers.Where(x => x.Job.Status == (int)JobStatus.Complete).GroupBy(g => g.WorkerName, g => g.Job.CategoryName, (Key, Group) => new { WorkerName = Key, Jobs = Group.ToList() }).ToList();
                response.MedicalServicesCompletionChart.Categories = new List<string>();
                response.GeneralServicesCompletionChart.Categories = new List<string>();
                response.Workers.WorkerName = new List<string>();
                response.Workers.Categories = new List<string>();
                response.MedicalServicesCompletionChart.Jobs = new List<int>();
                response.GeneralServicesCompletionChart.Jobs = new List<int>();
                response.Workers.Jobs = new List<int>();
                foreach (var cat in GeneralCategories)
                {
                    response.GeneralServicesCompletionChart.Categories.Add(cat.Name);
                    var CatJobs = Jobs.Where(x => x.CategoryId == cat.Id);
                    response.GeneralServicesCompletionChart.Jobs.Add(CatJobs.Count());
                } foreach (var cat in MedicalCategories)
                {
                    response.MedicalServicesCompletionChart.Categories.Add(cat.Name);
                    var CatJobs = Jobs.Where(x => x.CategoryId == cat.Id);
                    response.MedicalServicesCompletionChart.Jobs.Add(CatJobs.Count());
                }
                var CompleteJobs = Jobs.Where(x => x.Status == (int)JobStatus.Complete && x.JobWorker.Count > 0).GroupBy(x=>x.CategoryName, (Key, Group) => new { CategoryName = Key, Jobs = Group.ToList() }).ToList();
                //foreach (var cat in CompleteJobs)
                //{
                //    response.Workers.Categories.Add(cat.CategoryName);
                //    foreach (var Worker in Workers)
                //    {
                //        foreach(var Category in Worker.Jobs)
                //        {
                //            if (Category.Contains(cat.CategoryName))
                //            {
                //                response.Workers.WorkerName.Add(Worker.WorkerName);
                //                response.Workers.Jobs.Add(Category.Count());
                //            }
                //        }
                //    }
                //}

                foreach(var Worker in Workers)
                {
                    response.Workers.WorkerName.Add(Worker.WorkerName);
                    response.Workers.Jobs.Add(Worker.Jobs.Count());
                    foreach(var cat in Worker.Jobs)
                    {
                        response.Workers.Categories.Add(cat);
                    }
                }
                response.Success = true;
               
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }

    }
}
