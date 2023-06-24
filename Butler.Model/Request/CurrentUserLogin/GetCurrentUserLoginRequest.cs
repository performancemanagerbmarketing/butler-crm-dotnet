using Butler.Model.EntityModel;
using Butler.Model.Enum;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.CurrentUserLogin
{
    public class GetCurrentUserLoginResponse : Response
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string UserTypeEnum { get; set; }
    }
    public class GetCurrentUserLoginRequest
    {
        public string UserId { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(GetCurrentUserLoginRequest req)
        {
            var response = new GetCurrentUserLoginResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var LoginUser = _dbContext.UserProfile.Where(x => x.UserId == req.UserId).FirstOrDefault();
                response.Id = LoginUser.Id;
                response.FullName = LoginUser.FullName;
                response.ProfileImageUrl = LoginUser.ProfileImageUrl;
                response.UserTypeEnum = ((UserType)LoginUser.UserType.Value).ToString();
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
