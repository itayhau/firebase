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

        static private async Task Push()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN"
            };

            PushResponse response = await firebaseClient.PushTaskAsync("Customer/1", data);
            Data result = response.ResultAs<Data>();

            Console.WriteLine("Data pushed " + data.ID);
        }

        static private async Task Update()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN !"
            };

            FirebaseResponse response = await firebaseClient.UpdateTaskAsync("Customer/1", data);
            Data result = response.ResultAs<Data>();

            Console.WriteLine("Data updated " + data.ID);
        }


        static private async Task Delete()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN"
            };

            DeleteResponse response = await firebaseClient.DeleteTaskAsync("Customer/1");

            Console.WriteLine("Data deleted " + data.ID);
        }

        static private async Task Add()
        {
            var data = new Data
            {
                ID = "1",
                Name = "AUSTIN"
            };

            SetResponse response = await firebaseClient.SetTaskAsync("Customer/1", data);
            Data result = response.ResultAs<Data>();

            Console.WriteLine("Data inserted " + data.ID);
        }

        static private async Task GetAll()
        {

            FirebaseResponse response = await firebaseClient.GetTaskAsync("Customer");
            var result = response.ResultAs<RestSharp.JsonArray>();

        }

        static private async Task GetOne()
        {
            FirebaseResponse response = await firebaseClient.GetTaskAsync("Customer/1");
            
            Console.WriteLine("111111111");
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
                Add().Wait();
                //GetOne().Wait();
                //Push().Wait();
                //Update().Wait();
                //Delete().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Done");
        }
    }
}
