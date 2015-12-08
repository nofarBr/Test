using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace SaveMyDay.ServerLogic
{
    public class ServerLogic
    {
        // Ctor
        public ServerLogic()
        {
            
        }

        // Ctor with user details for login.
        public ServerLogic(User user)
        {

        }

        // Connect to the "system"
        public static bool Connect(User user)
        {
            return false;
        }

        // Active the algoritem
        public void Activate()
        {
            // Run the active methods on algoritem
        }

        // Get Recommended path by priority
        public Path GetResult(int priority)
        {
            return null;
        }

        // Get Accept from user of a path.
        public void Accept(List<Appointment> appointments)
        {
            // insert the appointments to the "company"
        }
    }
}
