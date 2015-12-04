using System.Collections.Generic;


namespace SaveMyDay.Entities
{
    public class Path
    {
        public int Code { get; set; }
        public User User { get; set; }
        public List<Appointment> Appointment { get; set; }
        public MovementType type { get; set; }

        public Path()
        {
            Appointment = new List<Appointment>();
        }

        public enum MovementType
        {
            Car,
            walk,
        }
    }
}
