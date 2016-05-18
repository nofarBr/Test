using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SaveMyDate.Entities;
using System.Json;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using MongoDB.Bson;

namespace TestClientForControlers
{
    public class WebApiHelper
    {
        private readonly string _url = "";
        private readonly HttpClient _client;

        public WebApiHelper(string url)
        {
            this._url = url;
            
            _client = new HttpClient();
            //_client.BaseAddress = new Uri("http://localhost:60799/api/Appointment");
            //_client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public Appointment[] Get(string customerId = "")
        {
            HttpResponseMessage response = _client.GetAsync("api/Appointment").Result;
            Appointment obj = JsonConvert.DeserializeObject<Appointment>(response.Content.ReadAsStringAsync().Result);
            Appointment[] data = new Appointment[1];
            data[0] = obj;
            return data;
        }

        public string Post()
        {
            var company = new Company()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                //Location = new Location() {X = 32.5669, Y = 36.5589},
                Type = CompanyType.Banks,
                UrlForApi = "this ia the best uri in the world hai tov baolam!!!!!",
                SubType = "פועלים"
            };

            var gizmo = new Appointment()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Remark = "hhh",
                //CompanyId = "567548f8d9f4b33b10815214",
                Time = DateTime.Now
            };

       
            var response1 = _client.PostAsJsonAsync("http://localhost:60799/api/Company", company).Result;
           var response = _client.PostAsJsonAsync("http://localhost:60799/api/Appointment", gizmo).Result;
            return response.Content.ToString();

        }

       
    }
}
