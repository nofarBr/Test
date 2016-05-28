using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace SaveMyDay.Algoritem
{
    public class AlgoritemRunner
    {
        public IList<Path> Results { get; private set; }
        
        private const int HOUR_MAX = 22;
        private const int HOUR_MIN = 6;

        private PathHandler[,] _routeMatrix;

        public AlgoritemRunner()
        {
            Results = new List<Path>();
        }

        public bool Activate(List<CompanySubType> errands, List<Constraint> constraints, Dictionary<CompanySubType, List<Appointment>> appointmentDataBase, Dictionary<Tuple<string,string>,int> deltaTimeMatrix)
        {
            // the number of options in every row
            var numberOfErrandCombinations = (int)Math.Pow(2, errands.Count);
            _routeMatrix = new PathHandler[HOUR_MAX - HOUR_MIN + 1, numberOfErrandCombinations];
            _routeMatrix[HOUR_MAX - HOUR_MIN,0] = new PathHandler(new Path { Constraints = constraints });
            var finalOptionList = new List<PathHandler>();

            // for every hour in day, counting from the end
            for (var currHour = HOUR_MAX-1; currHour >= HOUR_MIN; currHour--)
            {
                // set all empty path
                _routeMatrix[currHour - HOUR_MIN,0] = new PathHandler(new Path { Constraints = constraints });

                // for every errand combination in the row
                for (var i = 1; i < numberOfErrandCombinations; i++)
                {
                    // check raised bits
                    var bits = new bool[errands.Count];
                    var tmp = i;
                    for (var bit = 0; bit < errands.Count; bit++)
                    {
                        bits[bit] = tmp % 2 == 1;
                        tmp /= 2;
                    }

                    var optionList = new List<PathHandler>();
                    for (var bit = 0; bit < bits.Length; bit++)
                    {
                        if (bits[bit])
                        {
                            for(int j = currHour; j < HOUR_MAX; j++)
                                if(_routeMatrix[j - HOUR_MIN + 1, i - (int)Math.Pow(2, bit)] != null)
                                    optionList.AddRange(FindAppointmentsBetweenTimes(currHour, currHour + 1, _routeMatrix[j-HOUR_MIN+1,i-(int)Math.Pow(2,bit)],
                                        appointmentDataBase[errands[bit]], deltaTimeMatrix));
                            // 2 path combiner ?????? TODO:
                        }
                    }

                    //pick best for the slot from option list
                    if (optionList.Count != 0)
                    {
                        PathHandler best = optionList[0];
                        foreach (var option in optionList)
                        {
                            if (best.CalcWatedTimeInSeconds() > option.CalcWatedTimeInSeconds())
                                best = option;
                        }
                        _routeMatrix[currHour - HOUR_MIN, i] = best;
                    }
                    else
                    {
                        _routeMatrix[currHour - HOUR_MIN, i] = null;
                    }

                    if(i == numberOfErrandCombinations-1)
                        finalOptionList.AddRange(optionList);
                }
            }

            ExtractResultFromPathHandlerList(FindTheBestPath(finalOptionList));
            return true;
        }

        private List<PathHandler> FindAppointmentsBetweenTimes(int startHour, int endHour, 
            PathHandler currPath, List<Appointment> appointments, Dictionary<Tuple<string, string>, int> deltaTimeMatrix)
        {
            var result = new List<PathHandler>();
            foreach (var appointment in appointments.Where(
                    a => a.Time.Hour >= startHour && a.Time.Hour < endHour && currPath.IsAppointmentAddable(a, deltaTimeMatrix)).ToList())
            {
                var tmp = currPath.Clone();
                tmp.AddAppointment(appointment);
                result.Add(tmp);
            }
            return result;
        }
        
        private List<PathHandler> FindTheBestPath(List<PathHandler> paths)
        {
            return paths.OrderByDescending(p => p.Path.Appointments.Count).ThenBy(p => p.CalcWatedTimeInSeconds()).ToList();
        }

        private void ExtractResultFromPathHandlerList(List<PathHandler> list)
        {
            Results.Clear();
            foreach (var pathHandler in list)
            {
                Results.Add(pathHandler.Path);
            }
        }
    }
}