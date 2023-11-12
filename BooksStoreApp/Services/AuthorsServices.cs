using BooksStoreApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStoreApp.Services
{
    public class AuthorsServices
    {
        private string baseUrl { get; set; }
        public AuthorsServices()
        {
            this.baseUrl = DeviceInfo.Platform ==
                DevicePlatform.Android ? "http://your_ip_address:8080/api/Authors/" : "http://localhost:8080/api/Authors/";
        }

        public async Task<List<AuthorModel>> GetAllAuthors()
        {
            try
            {
                List<AuthorModel> authorsList = new List<AuthorModel>();

                string fullUrl = this.baseUrl + "GetAllAuthors";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentRespone = await httpResponseMessage.Content.ReadAsStringAsync();

                    authorsList = JsonConvert.DeserializeObject<List<AuthorModel>>(contentRespone);
                }

                return await Task.FromResult(authorsList.ToList());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
