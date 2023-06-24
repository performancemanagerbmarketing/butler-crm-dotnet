using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class SmsApiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage SendSms(string message, string number)
        {
            string ContactNumbers = "";
            //if (ConfigurationManager.AppSettings["CCSmsContact"] != null)
            //{
            //    ContactNumbers = ConfigurationManager.AppSettings["CCSmsContact"];
            //}
            string URL = "https://gateway.its.com.pk/api/otp?action=sendmessage&username=Butler&password=S@^_P!@7$101&recipient=" + ContactNumbers + number + "&originator=BUTLER&otpcode=" + message;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.

            }
            else
            {
                //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return response;
        }
    }
}