using Newtonsoft.Json;
using Read_From_CSV_And_Save_To_DB.DbContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_From_CSV_And_Save_To_DB
{
    public class Airport_IATA_codes
    {
        public int ID { get; set; }
        public string IATA_code { get; set; }
        public string AirportName { get; set; }
        public string TownName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Airport_IATA_codes_from_JSON> listOfAirportsFromJSON = new List<Airport_IATA_codes_from_JSON>();
            using (StreamReader r = new StreamReader(@"C:\Users\rkatalenic\Downloads\airport_IATA_code.json"))
            {
                string json = r.ReadToEnd();
                listOfAirportsFromJSON = JsonConvert.DeserializeObject<List<Airport_IATA_codes_from_JSON>>(json);
            }

            List<Airport_IATA_codes> listOfAirports = new List<Airport_IATA_codes>();
            foreach (var item in listOfAirportsFromJSON)
            {
                Airport_IATA_codes airport = new Airport_IATA_codes()
                {
                    AirportName = item.name,
                    IATA_code = item.iata_code,
                    TownName = item.municipality
                };

                listOfAirports.Add(airport);
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// READ FROM CSV (be careful when doing split on ROW, becauze it ignore nulls and therefore there are less items in string array)
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //var BooksFromCsv = from row in File.ReadLines(@"C:\Users\rkatalenic\Desktop\Airport_IATA_codes.csv")/*.Where(arg => !string.IsNullOrWhiteSpace(arg) && arg.Length > 0).AsEnumerable()*/
            //                   let column = row.Split(',')
            //                   select new Airport_IATA_codes
            //                   {
            //                       IATA_code = column[10],
            //                       AirportName = column[2],
            //                       TownName = column[8],
            //                   };



            DynamicDbContext DbContext = new DynamicDbContext();
            //DbContext.AirportIATACodes.AddRange(BooksFromCsv);
            DbContext.AirportIATACodes.AddRange(listOfAirports);

            DbContext.SaveChanges();
        }
    }

    public class Airport_IATA_codes_from_JSON
    {
        public string ident { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string elevation_ft { get; set; }
        public string continent { get; set; }
        public string iso_country { get; set; }
        public string iso_region { get; set; }
        public string municipality { get; set; }
        public string gps_code { get; set; }
        public string iata_code { get; set; }
        public string local_code { get; set; }
        public string coordinates { get; set; }
    }
}
