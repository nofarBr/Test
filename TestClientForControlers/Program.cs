using SaveMyDay.DistancesMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySimulator;
using SaveMyDate.Entities;
using TestClientForControlers;

namespace TestClientForControlers
{
    class Program
    {
        static void Main(string[] args)
        {
            // test for finding free appointments
            var pp = new FreeAppointmentFinder().FindFreeAppointmentByDay(DateTime.Now, CompanyType.Banks, "פועלים");
            //DistancesMatrixCreator.Run();
            //Dictionary<Tuple<string, string>, int> dictionary = DistancesMatrixReader.Read();

            /*WebApiHelper helper = new WebApiHelper("http://localhost:60799/");

            while(true)
                {
                helper.Post();
              //  var pp = helper.Get();
                Console.WriteLine("done");
            }
           
            */
        }
    }
}
