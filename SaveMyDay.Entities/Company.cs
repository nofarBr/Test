using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyDay.Entities
{
    public class Company
    {
        public int Code { get; set; }
        public Location Location { get; set; }
        public CompanyType Type { get; set; }
        public string UrlForApi { get; set; }
        public List<Appointment> appoiments { get; set; }

        public Company()
        {
            appoiments = new List<Appointment>();
        }

        public enum CompanyType
        {
            MedicalClinic,
            Banks,
            PostOffice
        }
    }
}
