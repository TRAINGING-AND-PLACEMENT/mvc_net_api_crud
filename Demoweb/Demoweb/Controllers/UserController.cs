using Demoweb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace Demoweb.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("http://192.168.1.101/api/all_api.php?what=");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {   
            List<UserView> model = new List<UserView>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress+"get_user").Result;
            if(response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                Debug.Write(data);
                model = JsonConvert.DeserializeObject<List<UserView>>(data);
            }
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserView model)
        {
            String data  = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_user", content).Result;
            if (response.IsSuccessStatusCode)
            {
                /*String data2 = response.Content.ReadAsStringAsync().Result;
                Debug.Write(data2);*/
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            UserView model = new UserView();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "edit_user&id="+id).Result;
            if(response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                /*Debug.Write(data);*/
                model = JsonConvert.DeserializeObject<UserView>(data);
            }
            return View(model);
        }

        [HttpPost]

        public IActionResult Edit(UserView model)
        {
            String data = JsonConvert.SerializeObject(model); 
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "update_user&id="+model.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                String data2 = response.Content.ReadAsStringAsync().Result;
                /*Debug.Write(data2);*/
                return RedirectToAction("index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            UserView model = new UserView();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "delete_user&id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                Debug.Write(data);
                return RedirectToAction("index");
            }
            return View();
        }
        /*public IActionResult Export(int id)
        {
            // Connection string to your SQL Server database
            //string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True";

            // SQL query to retrieve data from a table in your database
            //string query = "SELECT Column1, Column2, Column3 FROM YourTable";

            // Create a connection to the database and execute the query
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
                //connection.Open();
                //using (SqlCommand command = new SqlCommand(query, connection))
                //{
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a string builder to build the CSV file contents
                        StringBuilder csv = new StringBuilder();

                        // Add headers to the CSV file
                        csv.AppendLine("Column1,Column2,Column3");

                        // Loop through the data and add it to the CSV file
                        while (reader.Read())
                        {
                            csv.AppendLine(string.Format("{0},{1},{2}", reader["Column1"], reader["Column2"], reader["Column3"]));
                        }

                        // Export the data to a CSV file and return it as a file download
                        string fileName = "YourTable.csv";
                        byte[] fileContents = Encoding.UTF8.GetBytes(csv.ToString());
                        return File(fileContents, "text/csv", fileName);
                    }
                //}
            //}
        }*/

        public IActionResult Export(int id)
        {
            UserView model = new UserView();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "edit_user&id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserView>(data);
                /*Debug.Write(model.Name);*/

                string[] columnNames = new string[] { "id", "name", "email" };
                string csv = string.Empty;

                foreach(string column in columnNames)
                {
                    csv += column + ',';
                }
                csv += "\r\n";
                csv += model.Id.ToString().Replace(",", ";") + ',';
                csv += model.Name.Replace(",", ";") + ',';
                csv += model.Email.Replace(",", ";") + ',';

                csv += "\r\n";

                byte[] bytes = Encoding.ASCII.GetBytes(csv);
                return File(bytes, "text/csv", "user" + model.Id + ".csv");
            }
            return View(model);
        }
    }
}
