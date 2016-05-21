using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDate.Entities
{
    public class FreeAppointmentCompany : IMongoEntity
    {
        public string Id { get; set; }
        public Company Company { get; set; }
        public List<FreeAppointment> freeAppointments { get; set; }

        public FreeAppointmentCompany()
        {
            this.freeAppointments = new List<FreeAppointment>();
        }
    }
}
