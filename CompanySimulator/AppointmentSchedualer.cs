using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using SaveMyDate.Entities;

namespace CompanySimulator
{
    public class AppointmentSchedualer
    {
        public bool SchedualAppointment(Appointment appointment)
        {
            bool succeded;

            switch (appointment.Company.Type)
            {
                case CompanyType.Banks:
                    {
                        succeded = PostAppointment("Bank", appointment);
                        break;
                    }
                case CompanyType.MedicalClinic:
                    {
                        succeded = PostAppointment("MedicalClinic",appointment);
                        break;
                    }
                case CompanyType.PostOffice:
                    {
                        succeded = PostAppointment("PostOffice",appointment);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(appointment.Company.Type), appointment.Company.Type, null);
            }

            return succeded;
        }

        private bool PostAppointment(string controllerName,Appointment appointment)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61820/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response =  client.PostAsJsonAsync("api/" + controllerName, appointment).Result;
            return response.IsSuccessStatusCode;
           
        }
    }
}