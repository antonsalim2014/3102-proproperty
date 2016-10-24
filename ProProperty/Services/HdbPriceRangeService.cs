﻿using ProProperty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ProProperty.Services
{
    public class HdbPriceRangeService : IHdbPriceRangeService
    {
        private const string URL = "https://data.gov.sg/api/action/datastore_search?resource_id=d23b9636-5812-4b33-951e-b209de710dd5&limit=200";

        public List<HdbPriceRange> GetHdbPriceRange()
        {
            List <HdbPriceRange> priceRangeList = new List<HdbPriceRange>();

            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Accept = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            System.Diagnostics.Debug.WriteLine("Response stream received. : HDB PRICE RANGE SET DATA " + DateTime.Now.ToString("hh: mm:ss tt"));
            
            String jsonResponse = readStream.ReadToEnd();
            JsonResult jsonResult = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<JsonResult>(jsonResponse);
            foreach (Record i in jsonResult.result.records)
            {
                if (i.min_selling_price == "na") continue;
                HdbPriceRange hdbRange = new HdbPriceRange();
                hdbRange.hdb_id = i._id;
                hdbRange.town = i.town;
                hdbRange.room_type = i.room_type;
                hdbRange.min_selling_price_less_ahg_shg = i.min_selling_price_ahg_shg;
                hdbRange.min_selling_price = i.min_selling_price;
                hdbRange.max_selling_price_less_ahg_shg = i.max_selling_price_ahg_shg;
                hdbRange.max_selling_price = i.max_selling_price;
                hdbRange.financial_year = i.financial_year;

                priceRangeList.Add(hdbRange);

            }
            response.Close();
            readStream.Close();

            return priceRangeList;
        }
    }

    public class JsonResult
    {
        public string help { get; set; }
        public string success { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string resource_id { get; set; }
        public List<Field> fields { get; set; }
        public List<Record> records { get; set; }
        public Link _links { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
    }

    public class Record
    {
        public string town { get; set; }
        public string max_selling_price { get; set; }
        public string financial_year { get; set; }
        public int _id { get; set; }
        public string max_selling_price_ahg_shg { get; set; }
        public string room_type { get; set; }
        public string min_selling_price { get; set; }
        public string min_selling_price_ahg_shg { get; set; }
    }

    public class Field
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Link
    {
        public string start { get; set; }
        public string next { get; set; }
    }
}