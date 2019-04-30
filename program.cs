using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace FBConsole
{
    class Program
    {

        static IFirebaseClient firebaseClient;

        static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "yOn0WGMYZ0NahbuiTBlXrAfAC1HYgGSNMricEi7v",
            BasePath = "https://demo1-25b64.firebaseio.com/"
        };

        static private void Push()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN"
            };

            PushResponse response = firebaseClient.Push("Customer/1", data);
            Data result = response.ResultAs<Data>();

            Console.WriteLine("Data pushed " + data.ID);
        }

        static private void Update()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN !"
            };

            FirebaseResponse response = firebaseClient.Update("Customer/1", data);
            Data result = response.ResultAs<Data>();

            Console.WriteLine("Data updated " + data.ID);
        }


        static private void Delete()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN"
            };

            DeleteResponse response = firebaseClient.Delete("Customer/1");

            Console.WriteLine("Data deleted " + data.ID);
        }

        static private void Add()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN"
            };

            SetResponse response = firebaseClient.Set("Customer/1", data);
            Data result = response.ResultAs<Data>();

            Console.WriteLine("Data inserted " + data.ID);
        }

        static private void GetAll()
        {

            FirebaseResponse response = firebaseClient.Get("Customer");
            var result = response.ResultAs<List<Data>>();
            result.RemoveAt(0);
            result.ForEach(d => Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(d)));
        }

        static private void GetOne()
        {
            FirebaseResponse response = firebaseClient.Get("Customer/1");
            
            Data result = response.ResultAs<Data>();

            Console.WriteLine(result.ID);
            Console.WriteLine(result.Name);
        }

        static void Main(string[] args)
        {
            firebaseClient = new FireSharp.FirebaseClient(config);
            if (firebaseClient != null)
            {
                Console.WriteLine("Connection Succeeded!");
            }
            try
            {
                Add();
                GetOne();
                Push();
                Update();
                GetAll();
                Delete();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Done");
        }
    }
}
