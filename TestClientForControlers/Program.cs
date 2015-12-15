using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClientForControlers;

namespace TestClientForControlers
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApiHelper helper = new WebApiHelper("http://localhost:60799/api/Appointment/");

            while(true)
                {
                helper.Post();
              //  var pp = helper.Get();
                Console.WriteLine("done");
            }
           

        }
    }
}
