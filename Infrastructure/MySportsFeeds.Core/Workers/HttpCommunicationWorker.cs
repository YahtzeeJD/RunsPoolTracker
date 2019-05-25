using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MySportsFeeds.Core.Workers
{
    internal class HttpCommunicationWorker
    {
        private Uri baseUrl;
        public string Version { get; set; }
        private AuthenticationHeaderValue authenticationHeader = null;

        public HttpCommunicationWorker(string baseUrl, string version, string username, string password)
        {
            Version = version;
            this.baseUrl = new Uri(baseUrl);

            SetBasicAuthentication(username, password);
        }

        public void SetBasicAuthentication(string base64Auth)
        {
            this.authenticationHeader = new AuthenticationHeaderValue("Basic", base64Auth);
        }

        public void SetBasicAuthentication(string username, string password)
        {
            byte[] userPassBytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
            string userPassBase64 = Convert.ToBase64String(userPassBytes);

            SetBasicAuthentication(userPassBase64);
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = this.baseUrl;

            if (this.authenticationHeader != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = this.authenticationHeader;
            }

            return httpClient;
        }

        public async Task<T> GetAsync<T>(string requestUrl)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUrl).ConfigureAwait(false))
            {
                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    ex.Data["json"] = json;
                    throw;
                }
            }
        }

        public async Task<string> GetAsync(string requestUrl)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUrl).ConfigureAwait(false))
            {
                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                return json;
            }
        }
    }
}