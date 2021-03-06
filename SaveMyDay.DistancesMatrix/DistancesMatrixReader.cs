﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDay.DistancesMatrix
{
    public class DistancesMatrixReader
    {
        public static Dictionary<Tuple<string, string>, int> Read(string strCity)
        {
            // Connecting to the DB
            MongoClient client = new MongoClient(@"mongodb://localhost:27017/SaveMayDay");
            var server = client.GetServer();
            var database = server.GetDatabase("SaveMayDay");
            MongoCollection<BsonDocument> distancesCollection = database.GetCollection("distances");

            Dictionary<Tuple<string, string>, int> dictDistances = new Dictionary<Tuple<string, string>, int>();

            // Reading the distances document from the DB
            IMongoQuery query = Query.Matches("city", strCity);
            BsonDocument bsonDoc = distancesCollection.Find(query).FirstOrDefault();

            // Validate that we found the document
            if (bsonDoc != null)
            {
                BsonValue bsonContent = bsonDoc["content"];

                // Run over all the distances items
                for (int i = 0; i < bsonContent.AsBsonArray.Count; i++)
                {
                    string strId1 = bsonContent.AsBsonArray.ElementAt(i)["id1"].ToString();
                    string strId2 = bsonContent.AsBsonArray.ElementAt(i)["id2"].ToString();
                    int nDistance = bsonContent.AsBsonArray.ElementAt(i)["distance"].ToInt32();

                    // Add the current distance to the dictionary
                    Tuple<string, string> tupleDistance = new Tuple<string, string>(strId1, strId2);
                    dictDistances.Add(tupleDistance, nDistance);
                }
            }

            // Return the dictionary
            return (dictDistances);
        }
    }
}
