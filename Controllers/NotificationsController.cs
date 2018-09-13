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

        private HttpClient httpClient = new HttpClient();

        public NotificationsController(IOptions<PushoverConfiguration> config)
        {
            this.config = config;
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Send([FromBody]string message)
        {
            var values = new Dictionary<string, string> { { "token", this.config.Value.Token }, { "user", this.config.Value.User }, { "message", message } };

            var content = new FormUrlEncodedContent(values);

            var response = await this.httpClient.PostAsync("https://api.pushover.net/1/messages.json", content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}