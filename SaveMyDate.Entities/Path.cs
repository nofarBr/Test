using System.Collections.Generic;


namespace SaveMyDate.Entities
{

    //delete this
    public class Path
    {
        public int Code { get; set; }
        public User User { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Constraint> Constraints { get; set; }
        public MovementType type { get; set; }

        public Path()
        {
            Appointments = new List<Appointment>();
            Constraints = new List<Constraint>();
        }

        public enum MovementType
        {
            Car,
            walk,
        }
    }
}
