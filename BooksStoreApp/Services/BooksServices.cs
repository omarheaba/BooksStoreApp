using BooksStoreApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BooksStoreApp.Services
{
    public class BooksServices
    {
        private string baseUrl { get; set; }

        public BooksServices()
        {
            this.baseUrl = DeviceInfo.Platform == 
                DevicePlatform.Android ? "http://your_ip_address:8080/api/Books/" : "http://localhost:8080/api/Books/";
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            try
            {
                List<BookModel> booksList = new List<BookModel>();

                string fullUrl = this.baseUrl + "GetAllBooks";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentRespone = await httpResponseMessage.Content.ReadAsStringAsync();

                    booksList = JsonConvert.DeserializeObject<List<BookModel>>(contentRespone);
                }

                return await Task.FromResult(booksList.ToList());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BookModel> GetBookById(int bookId)
        {
            try
            {

                BookModel currentBook = new BookModel();

                string fullUrl = this.baseUrl + $"GetBookById/{bookId}";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentRespone = await httpResponseMessage.Content.ReadAsStringAsync();

                    currentBook = JsonConvert.DeserializeObject<BookModel>(contentRespone);
                }

                return await Task.FromResult(currentBook);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BookModel> AddNewBook(BookModel bookInfo)
        {
            try
            {

                string fullUrl = this.baseUrl + $"AddNewBook";

                string bookInfoAsJson = JsonConvert.SerializeObject(bookInfo);

                StringContent bookStringContent = new StringContent(bookInfoAsJson, Encoding.UTF8, "application/json");


                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", bookStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(bookInfo);
                }

                return await Task.FromResult(new BookModel());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BookModel> EditBook(BookModel bookInfo)
        {
            try
            {
                string fullUrl = this.baseUrl + $"EditBook/{bookInfo.BookId}";

                string bookInfoAsJson = JsonConvert.SerializeObject(bookInfo);

                StringContent bookStringContent = new StringContent(bookInfoAsJson, Encoding.UTF8, "application/json");


                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("", bookStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(bookInfo);
                }

                return await Task.FromResult(new BookModel());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteBook(BookModel bookInfo)
        {
            try
            {
                string fullUrl = this.baseUrl + $"DeleteBook/{bookInfo.BookId}";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }

                return await Task.FromResult(false);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
