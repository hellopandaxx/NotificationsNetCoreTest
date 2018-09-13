namespace TestNotificationsService.Controllers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("api/notifications")]
    public class NotificationsController : Controller
    {
        private readonly IOptions<PushoverConfiguration> config;

        private const string ErrorMessageStub = "At the moment the service is not available. Please try again latter.";

        private HttpClient httpClient = new HttpClient();

        public NotificationsController(IOptions<PushoverConfiguration> config)
        {
            this.config = config;
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Send([FromBody]string message)
        {
            try
            {
                var values = new Dictionary<string, string> { { "token", this.config.Value.Token }, { "user", this.config.Value.User }, { "message", message } };

                var content = new FormUrlEncodedContent(values);

                var response = await this.httpClient.PostAsync("https://api.pushover.net/1/messages.json", content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "ok";
                }

                // TODO:
                // Log(response)
                
                return ErrorMessageStub;
            }
            catch
            {
                // TODO:
                // Log (exception)
                return ErrorMessageStub;
            }
        }
    }
}