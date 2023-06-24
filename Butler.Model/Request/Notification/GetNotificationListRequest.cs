using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Notification
{
    public class GetNotificationListResponse : Response
    {
        public List<Notification> Data { get; set; }
    }
    public class Notification
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AgentId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsRead { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Duration { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Date { get; set; }

    }
    public class GetNotificationListRequest
    {
        private ButlerEntities _dbContext = new ButlerEntities();
        public int? CustomerId { get; set; }
        public string UserId { get; set; }
        public object RunRequest(GetNotificationListRequest req)
        {
            var response = new GetNotificationListResponse();
            response.ValidationErrors = new List<string>();
            response.Data = new List<Notification>();
            try
            {
                var Notifications = new List<Model.EntityModel.Notification>();//_dbContext.Notification.Where(x=>x.Type == req.Type).ToList();
                if (req.CustomerId.HasValue)
                {
                    Notifications = _dbContext.Notification.Where(x => x.CustomerId == req.CustomerId && x.IsRead == false).OrderByDescending(x => x.Id).ToList();
                }
                else if(req.UserId != null)
                {
                    var Agent = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                    Notifications = _dbContext.Notification.Where(x => x.AdminId == Agent.Id && x.IsRead == false).OrderByDescending(x=>x.Id).ToList();
                    
                }
                else
                {
                    Notifications = _dbContext.Notification.OrderByDescending(x => x.Id).ToList();
                }
                foreach (var notification in Notifications)
                {
                    var Notification = new Notification();
                    Notification.Id = notification.Id;
                    Notification.Content = notification.Content;
                    Notification.CustomerId = notification.CustomerId??0;
                    Notification.Title = notification.Title;
                   
                    if(notification.Date.HasValue)
                    Notification.Date = notification.Date.Value.ToString("MMMM dd, yyyy");
                    Notification.CreatedAt = notification.CreatedAt;
                    Notification.Link = notification.Link;
                    Notification.IsRead = notification.IsRead;
                    var Difference = (DateTime.Now - Notification.CreatedAt);
                    if (Difference.HasValue)
                    {
                        if ((Difference).Value.TotalDays > 364)
                        {
                            int Years = (int)(Difference.Value.TotalDays / 365);
                            Notification.Duration = Years + " Years Ago.";

                        }
                        else if (Difference.Value.TotalDays > 30)
                        {
                            int months = (int)(Difference.Value.TotalDays / 31);
                            Notification.Duration = months + " Months Ago.";
                        }
                        else if (Difference.Value.TotalDays >= 1)
                        {
                            Notification.Duration = (int)(Difference.Value.TotalDays) + " Days Ago.";
                        }
                        else if (Difference.Value.TotalDays < 1)
                        {
                            Notification.Duration = (int)(Difference.Value.TotalHours) + " Hours Ago.";
                        }
                        else if (Difference.Value.TotalHours < 1)
                        {
                            Notification.Duration = (int)(Difference.Value.Minutes) + " Minutes Ago.";
                        }
                        else if (Difference.Value.TotalMinutes < 1)
                        {
                            Notification.Duration = " Few Seconds Ago.";
                        }
                    }
                    response.Data.Add(Notification);
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
