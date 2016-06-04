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
        public static Dictionary<Tuple<string, string>, int> BuildDistancesMatrix(string strCity, 
            List<CompanySubType> lstCompanyTypes, List<Constraint> lstConstraints)
        {
            MongoCrud<Company> _mongoCrud = new MongoCrud<Company>();
            List<Company> lstAllCompanies = _mongoCrud.GetAllEntities().ToList<Company>();
            List<Company> lstRelevantCompanies = lstAllCompanies.Where(
                c => c.Location.Contains(strCity) && lstCompanyTypes.Contains(c.SubType)).ToList<Company>();

            var matrixDictionary = DistancesMatrixReader.Read(strCity);

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
            int nCompaniesBulkSize = 8;
            int nConstraintsBulkSize = 5;
            int nCompaniesIterations = Convert.ToInt32(Math.Ceiling(lstRelevantCompanies.Count / Convert.ToDouble(nCompaniesBulkSize)));
            int nConstraintsIterations = Convert.ToInt32(Math.Ceiling(lstConstraints.Count / Convert.ToDouble(nConstraintsBulkSize)));
            string strRequestUrl;
            WebRequest request;
            WebResponse response;
            XDocument xdoc;
            IEnumerable<XElement> lstRows;

            // Companies + Constraints Section

            for (int i = 0; i < nCompaniesIterations; i++)
            {
                string strCompaniesAddresses = "";
                for (int j = 0; (j < nCompaniesBulkSize) && (((i * nCompaniesBulkSize) + j) < lstRelevantCompanies.Count); j++)
                {
                    strCompaniesAddresses += lstRelevantCompanies[(i * nCompaniesBulkSize) + j].Location + "|";
                }
                strCompaniesAddresses = strCompaniesAddresses.Remove(strCompaniesAddresses.Length - 1, 1);

                for (int k = 0; k < nConstraintsIterations; k++)
                {
                    string strConstraintsAddresses = "";
                    for (int m = 0; (m < nConstraintsBulkSize) && (((k * nConstraintsBulkSize) + m) < lstConstraints.Count); m++)
                    {
                        strConstraintsAddresses += lstConstraints[(k * nConstraintsBulkSize) + m].Location + "|";
                    }
                    strConstraintsAddresses = strConstraintsAddresses.Remove(strConstraintsAddresses.Length - 1, 1);

                    strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strCompaniesAddresses + "&destinations=" + strConstraintsAddresses + "&key=AIzaSyAkT3L_pAicJBPXlRfkOjo3yIB-RueCk9I";

                    // Send request to google and get the response
                    request = WebRequest.Create(strRequestUrl);
                    response = request.GetResponse();

                    // Convert the data to xml document
                    xdoc = XDocument.Load(response.GetResponseStream());
                    lstRows = xdoc.Element("DistanceMatrixResponse").Elements("row");

                    // Run over all the results elements
                    for (int n = 0; n < lstRows.Count(); n++)
                    {
                        IEnumerable<XElement> lstCols = xdoc.Element("DistanceMatrixResponse").Elements("row").ElementAt(n).Elements();
                        for (int p = 0; p < lstCols.Count(); p++)
                        {
                            Tuple<string, string> tupleNew = new Tuple<string, string>(lstRelevantCompanies[(i * nCompaniesBulkSize) + n].Id, Convert.ToString((k * nConstraintsBulkSize) + p));
                            dictMatrix.Add(tupleNew, int.Parse(lstCols.ElementAt(p).Element("duration").Element("value").Value));
                        }
                    }

                    Thread.Sleep(12000);

                    strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strConstraintsAddresses + "&destinations=" + strCompaniesAddresses + "&key=AIzaSyAkT3L_pAicJBPXlRfkOjo3yIB-RueCk9I";

                    // Send request to google and get the response
                    request = WebRequest.Create(strRequestUrl);
                    response = request.GetResponse();

                    // Convert the data to xml document
                    xdoc = XDocument.Load(response.GetResponseStream());
                    lstRows = xdoc.Element("DistanceMatrixResponse").Elements("row");

                    // Run over all the results elements
                    for (int n = 0; n < lstRows.Count(); n++)
                    {
                        IEnumerable<XElement> lstCols = xdoc.Element("DistanceMatrixResponse").Elements("row").ElementAt(n).Elements();
                        for (int p = 0; p < lstCols.Count(); p++)
                        {
                            Tuple<string, string> tupleNew = new Tuple<string, string>(Convert.ToString((k * nConstraintsBulkSize) + n), lstRelevantCompanies[(i * nCompaniesBulkSize) + p].Id);
                            dictMatrix.Add(tupleNew, int.Parse(lstCols.ElementAt(p).Element("duration").Element("value").Value));
                        }
                    }

                    Thread.Sleep(12000);
                }
            }

            // Only Constraints Section

            int nOnlyConstraintsBulkSize = 6;
            int nOnlyConstraintsIterations = Convert.ToInt32(Math.Ceiling(lstConstraints.Count / Convert.ToDouble(nOnlyConstraintsBulkSize)));

            for (int i = 0; i < nOnlyConstraintsIterations; i++)
            {
                string strOutsideAddresses = "";
                for (int j = 0; (j < nOnlyConstraintsBulkSize) && (((i * nOnlyConstraintsBulkSize) + j) < lstConstraints.Count); j++)
                {
                    strOutsideAddresses += lstConstraints[(i * nOnlyConstraintsBulkSize) + j].Location + "|";
                }
                strOutsideAddresses = strOutsideAddresses.Remove(strOutsideAddresses.Length - 1, 1);

                for (int k = 0; k < nOnlyConstraintsIterations; k++)
                {
                    string strInsideAddresses = "";
                    for (int m = 0; (m < nOnlyConstraintsBulkSize) && (((k * nOnlyConstraintsBulkSize) + m) < lstConstraints.Count); m++)
                    {
                        strInsideAddresses += lstConstraints[(k * nOnlyConstraintsBulkSize) + m].Location + "|";
                    }
                    strInsideAddresses = strInsideAddresses.Remove(strInsideAddresses.Length - 1, 1);

                    strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strOutsideAddresses + "&destinations=" + strInsideAddresses + "&key=AIzaSyAkT3L_pAicJBPXlRfkOjo3yIB-RueCk9I";

                    // Send request to google and get the response
                    request = WebRequest.Create(strRequestUrl);
                    response = request.GetResponse();

                    // Convert the data to xml document
                    xdoc = XDocument.Load(response.GetResponseStream());
                    lstRows = xdoc.Element("DistanceMatrixResponse").Elements("row");

                    // Run over all the results elements
                    for (int n = 0; n < lstRows.Count(); n++)
                    {
                        IEnumerable<XElement> lstCols = xdoc.Element("DistanceMatrixResponse").Elements("row").ElementAt(n).Elements();
                        for (int p = 0; p < lstCols.Count(); p++)
                        {
                            Tuple<string, string> tupleNew = new Tuple<string, string>(Convert.ToString((i * nOnlyConstraintsBulkSize) + n), Convert.ToString((k * nOnlyConstraintsBulkSize) + p));
                            dictMatrix.Add(tupleNew, int.Parse(lstCols.ElementAt(p).Element("duration").Element("value").Value));
                        }
                    }

                    Thread.Sleep(12000);
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
    }
}
