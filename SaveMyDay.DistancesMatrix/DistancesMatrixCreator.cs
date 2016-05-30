using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using PathFinder;
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
    public class DistancesMatrixCreator
    {
        public static void Run()
        {
            // Get all the companies from the DB
            MongoCrud<Company> _mongoCrud = new MongoCrud<Company>();
            List<Company> listAllCompanies = _mongoCrud.GetAllEntities().ToList<Company>();

            //ComapanyQueryHandler query = new ComapanyQueryHandler();
            //List<Company> listAllCompanies = query.GetCompaniesByTypeAndLocation(null, "ראשון לציון");

            BsonArray bsonItems = new BsonArray();

            int nBulkSize = 10;

            // Run over all the companies
            for (int i = 0; i < listAllCompanies.Count; i++)
            {
                int nIterationsNumber = Convert.ToInt32(Math.Ceiling(listAllCompanies.Count / 10.0));
                //int nIterationsNumber = Convert.ToInt32(Math.Round(listAllCompanies.Count / 25.0, MidpointRounding.AwayFromZero));

                for (int j = 0; j < nIterationsNumber; j++)
                {
                    string strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + listAllCompanies[i].Location + "&destinations=";
                    for (int k = 0; (k < nBulkSize) && (((j * nBulkSize) + k) < listAllCompanies.Count); k++)
                    {
                        strRequestUrl += listAllCompanies[(j * nBulkSize) + k].Location + "|";
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
                        // Build the current way distance (id1->id2) and add it to the items array
                        BsonDocument currentDis = new BsonDocument
                        {
                            { "id1", listAllCompanies[i].Id },
                            { "id2", listAllCompanies[(j * nBulkSize) + k].Id },
                            { "distance", int.Parse(lstElements.ElementAt(k).Element("duration").Element("value").Value) }
                        };
                        bsonItems.Add(currentDis);
                    }

                    Thread.Sleep(12000);
                }

                // Build the request url for google
                /*string strRequestUrl = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + listAllCompanies[i].Location + "&destinations=";
                for (int j = 0; j < listAllCompanies.Count; j++)
                {
                    strRequestUrl += listAllCompanies[j].Location + "|";
                }
                strRequestUrl = strRequestUrl.Remove(strRequestUrl.Length - 1, 1);

                // Send request to google and get the response
                var request = WebRequest.Create(strRequestUrl);
                var response = request.GetResponse();

                // Convert the data to xml document
                var xdoc = XDocument.Load(response.GetResponseStream());
                IEnumerable<XElement> lstElements = xdoc.Element("DistanceMatrixResponse").Element("row").Elements();

                // Run over all the results elements
                for (int j = 0; j < lstElements.Count(); j++)
                {
                    // Build the current way distance (id1->id2) and add it to the items array
                    BsonDocument currentDis = new BsonDocument
                    {
                        { "id1", listAllCompanies[i].Id },
                        { "id2", listAllCompanies[j].Id },
                        { "distance", int.Parse(lstElements.ElementAt(j).Element("duration").Element("value").Value) }
                    };
                    bsonItems.Add(currentDis);
                }*/
            }

            // Connecting to the DBd
            MongoClient client = new MongoClient(@"mongodb://localhost:27017/SaveMayDay");
            var server = client.GetServer();
            var database = server.GetDatabase("SaveMayDay");
            MongoCollection distancesCollection = database.GetCollection("distances");

            BsonDocument bsonDocToSave = new BsonDocument
            {
                { "content", bsonItems }
            };

            // Save all the distances to the DB
            distancesCollection.Save(bsonDocToSave);
        }
    }
}
