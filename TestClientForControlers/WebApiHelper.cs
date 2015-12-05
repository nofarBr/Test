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
namespace TestClientForControlers
{
    public class WebApiHelper
    {
        private string url = "";
        HttpClient client = new HttpClient();

        public WebApiHelper(string url)
        {
            this.url = url;
        }

        public Appointment[] Get(string customerId = "")
        {
            HttpResponseMessage response = client.GetAsync(this.url).Result;
            Appointment obj = JsonConvert.DeserializeObject<Appointment>(response.Content.ReadAsStringAsync().Result);
            Appointment[] data = new Appointment[1];
            data[0] = obj;
            return data;
        }

        public string Post()
        {
            var gizmo = new Appointment()
            {
                Remark = "wtfff",
                Company = new Company()
                {
                    Code = 1,
                    Type = CompanyType.Banks,
                    Location = new Location()
                    {
                        Address = new Address()
                        {
                            City = new City()
                            {
                                Code = 1,
                                Decription = "wooo city"
                            },
                            HouseNumber = "6"
                        }
                    }
                },
                Time = DateTime.Now
                          };
            var response = client.PostAsJsonAsync(this.url, gizmo).Result;
            return response.Content.ToString();

        }

       
    }
}
