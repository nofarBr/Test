using SaveMayDay.Common;
using SaveMyDate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SaveMyDay.DistancesMatrix
{
    public class DistancesMatrixHandler
    {
        public class BasicMatrixItem
        {
            public string Id { get; set; }
            public string Location { get; set; }

            public BasicMatrixItem(string strId, string strLocation)
            {
                Id = strId; Location = strLocation;
            }
        }

        public static Dictionary<Tuple<string, string>, int> BuildDistancesMatrix(string strCity, 
            List<CompanySubType> lstCompanyTypes, List<Constraint> lstConstraints)
        {
            MongoCrud<Company> _mongoCrud = new MongoCrud<Company>();
            List<Company> lstAllCompanies = _mongoCrud.GetAllEntities().ToList<Company>();
            List<Company> lstRelevantCompanies = lstAllCompanies.Where(
                c => c.Location.Contains(strCity) && lstCompanyTypes.Contains(c.SubType)).ToList<Company>();

            var matrixDictionary = DistancesMatrixReader.Read();

            var filteredMatrix = DistancesMatrixHandler.FilterMatrixBySpecificCompanies(matrixDictionary, lstRelevantCompanies);

            if (lstConstraints.Count > 0)
            {
                filteredMatrix = DistancesMatrixHandler.AddConstraintsToDistancesMatrix(filteredMatrix, lstRelevantCompanies, lstConstraints);
            }

            return (filteredMatrix);
        }

        private static Dictionary<Tuple<string, string>, int> AddConstraintsToDistancesMatrix(
            Dictionary<Tuple<string, string>, int> dictMatrix, List<Company> lstRelevantCompanies,
            List<Constraint> lstConstraints)
        {
            string strCompaniesAddresses = "";
            for (int i = 0; i < lstRelevantCompanies.Count; i++)
            {
                strCompaniesAddresses += lstRelevantCompanies[i].Location + "|";
            }
            strCompaniesAddresses = strCompaniesAddresses.Remove(strCompaniesAddresses.Length - 1, 1);

            string strConstraintsAddresses = "";
            for (int j = 0; j < lstConstraints.Count; j++)
            {
                strConstraintsAddresses += lstConstraints[j].Location + "|";
            }
            strConstraintsAddresses = strConstraintsAddresses.Remove(strConstraintsAddresses.Length - 1, 1);


            string strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strCompaniesAddresses + "&destinations=" + strConstraintsAddresses + "&key=AIzaSyCOnipZ0p4Khy5BhgtWhmLdkO9j4Du1-iw";

            // Send request to google and get the response
            var request = WebRequest.Create(strRequestUrl);
            var response = request.GetResponse();

            // Convert the data to xml document
            var xdoc = XDocument.Load(response.GetResponseStream());
            IEnumerable<XElement> lstRows = xdoc.Element("DistanceMatrixResponse").Elements("row");

            // Run over all the results elements
            for (int k = 0; k < lstRows.Count(); k++)
            {
                IEnumerable<XElement> lstCols = xdoc.Element("DistanceMatrixResponse").Elements("row").ElementAt(k).Elements();
                for (int m = 0; m < lstCols.Count(); m++)
                {
                    Tuple<string, string> tupleNew = new Tuple<string, string>(lstRelevantCompanies[k].Id, Convert.ToString(m));
                    dictMatrix.Add(tupleNew, int.Parse(lstCols.ElementAt(m).Element("duration").Element("value").Value));
                }
            }

            Thread.Sleep(12000);

            strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strConstraintsAddresses + "&destinations=" + strCompaniesAddresses + "&key=AIzaSyCOnipZ0p4Khy5BhgtWhmLdkO9j4Du1-iw";

            // Send request to google and get the response
            request = WebRequest.Create(strRequestUrl);
            response = request.GetResponse();

            // Convert the data to xml document
            xdoc = XDocument.Load(response.GetResponseStream());
            lstRows = xdoc.Element("DistanceMatrixResponse").Elements("row");

            // Run over all the results elements
            for (int k = 0; k < lstRows.Count(); k++)
            {
                IEnumerable<XElement> lstCols = xdoc.Element("DistanceMatrixResponse").Elements("row").ElementAt(k).Elements();
                for (int m = 0; m < lstCols.Count(); m++)
                {
                    Tuple<string, string> tupleNew = new Tuple<string, string>(Convert.ToString(k), lstRelevantCompanies[m].Id);
                    dictMatrix.Add(tupleNew, int.Parse(lstCols.ElementAt(m).Element("duration").Element("value").Value));
                }
            }

            Thread.Sleep(12000);

            strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strConstraintsAddresses + "&destinations=" + strConstraintsAddresses + "&key=AIzaSyCOnipZ0p4Khy5BhgtWhmLdkO9j4Du1-iw";

            // Send request to google and get the response
            request = WebRequest.Create(strRequestUrl);
            response = request.GetResponse();

            // Convert the data to xml document
            xdoc = XDocument.Load(response.GetResponseStream());
            lstRows = xdoc.Element("DistanceMatrixResponse").Elements("row");

            // Run over all the results elements
            for (int k = 0; k < lstRows.Count(); k++)
            {
                IEnumerable<XElement> lstCols = xdoc.Element("DistanceMatrixResponse").Elements("row").ElementAt(k).Elements();
                for (int m = 0; m < lstCols.Count(); m++)
                {
                    Tuple<string, string> tupleNew = new Tuple<string, string>(Convert.ToString(k), Convert.ToString(m));
                    dictMatrix.Add(tupleNew, int.Parse(lstCols.ElementAt(m).Element("duration").Element("value").Value));
                }
            }

            return (dictMatrix);
        }

        private static Dictionary<Tuple<string, string>, int> FilterMatrixBySpecificCompanies(
            Dictionary<Tuple<string, string>, int> dictMatrix, List<Company> lstRelevantCompanies)
        {
            MongoCrud<Company> _mongoCrud = new MongoCrud<Company>();
            List<Company> listAllCompanies = _mongoCrud.GetAllEntities().ToList<Company>();
            List<string> lstFilteredCompanies = lstRelevantCompanies.Select(s => s.Id).ToList<string>();

            Dictionary<Tuple<string, string>, int> dictFilteredMatrix = new Dictionary<Tuple<string, string>, int>();

            foreach (Tuple<string, string> curr in dictMatrix.Keys)
            {
                if (lstFilteredCompanies.Contains(curr.Item1) && lstFilteredCompanies.Contains(curr.Item2))
                {
                    dictFilteredMatrix.Add(curr, dictMatrix[curr]);
                }
            }

            return (dictFilteredMatrix);
        }

        /*private static Dictionary<Tuple<string, string>, int> AddConstraintsToDistancesMatrix(
            Dictionary<Tuple<string, string>, int> dictMatrix, List<Company> lstRelevantCompanies, 
            List<Constraint> lstConstraints)
        {
            //Dictionary<Tuple<string, string>, int> dictMatrixWithConstraints = new Dictionary<Tuple<string, string>, int>(dictMatrix);

            for (int i = 0; i < lstRelevantCompanies.Count; i++)
            {
                string strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + lstRelevantCompanies[i].Location + "&destinations=";
                for (int j = 0; j < lstConstraints.Count; j++)
                {
                    strRequestUrl += lstConstraints[j].Location + "|";
                }
                strRequestUrl = strRequestUrl.Remove(strRequestUrl.Length - 1, 1);
                strRequestUrl += "&key=AIzaSyCOnipZ0p4Khy5BhgtWhmLdkO9j4Du1-iw";

                // Send request to google and get the response
                var request = WebRequest.Create(strRequestUrl);
                var response = request.GetResponse();

                // Convert the data to xml document
                var xdoc = XDocument.Load(response.GetResponseStream());
                IEnumerable<XElement> lstElements = xdoc.Element("DistanceMatrixResponse").Element("row").Elements();

                // Run over all the results elements
                for (int k = 0; k < lstElements.Count(); k++)
                {
                    Tuple<string, string> tupleNew = new Tuple<string, string>(lstRelevantCompanies[i].Id, Convert.ToString(k));
                    dictMatrix.Add(tupleNew, int.Parse(lstElements.ElementAt(k).Element("duration").Element("value").Value));
                }

                Thread.Sleep(12000);
            }

            List<BasicMatrixItem> lstItems = lstRelevantCompanies.Select(p => new BasicMatrixItem(p.Id, p.Location)).ToList();
            for (int i = 0; i < lstConstraints.Count; i++)
            {
                lstItems.Add(new BasicMatrixItem(Convert.ToString(i), lstConstraints[i].Location));
            }

            for (int i = 0; i < lstConstraints.Count; i++)
            {
                string strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + lstConstraints[i].Location + "&destinations=";
                for (int j = 0; j < lstItems.Count; j++)
                {
                    strRequestUrl += lstItems[j].Location + "|";
                }
                strRequestUrl = strRequestUrl.Remove(strRequestUrl.Length - 1, 1);
                strRequestUrl += "&key=AIzaSyCOnipZ0p4Khy5BhgtWhmLdkO9j4Du1-iw";

                // Send request to google and get the response
                var request = WebRequest.Create(strRequestUrl);
                var response = request.GetResponse();

                // Convert the data to xml document
                var xdoc = XDocument.Load(response.GetResponseStream());
                IEnumerable<XElement> lstElements = xdoc.Element("DistanceMatrixResponse").Element("row").Elements();

                // Run over all the results elements
                for (int k = 0; k < lstElements.Count(); k++)
                {
                    Tuple<string, string> tupleNew = new Tuple<string, string>(Convert.ToString(i), lstItems[k].Id);
                    dictMatrix.Add(tupleNew, int.Parse(lstElements.ElementAt(k).Element("duration").Element("value").Value));
                }

                Thread.Sleep(12000);
            }

            return (dictMatrix);
        }*/
    }
}
