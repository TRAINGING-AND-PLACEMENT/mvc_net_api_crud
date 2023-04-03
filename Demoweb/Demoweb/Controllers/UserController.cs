using CsvHelper;
using Demoweb.api;
using Demoweb.Controllers.json;
using Demoweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Demoweb.Controllers
{
    public class UserController : Controller
    {
        HttpClient client;
        JsonView jv = new JsonView();
        public UserController()
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Login() { return View(); }

        [HttpPost]
        public IActionResult Login(UserView model)
        {
            if (ModelState.IsValid)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "getlogin", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    Login logindetails = jv.LoginDetails(result);
                    var success = logindetails.success;
                    if (success)
                    {
                        int role = Convert.ToInt32(logindetails.user.role);
                        int userid = Convert.ToInt32(logindetails.user.id);
                        int sessionid = Convert.ToInt32(logindetails.student.session_id);
                        HttpContext.Session.SetInt32("role", role);
                        HttpContext.Session.SetInt32("userid", userid);
                        HttpContext.Session.SetInt32("sessionid", sessionid);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "Wrong id or password";
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(model);
        }

        public IActionResult Index()
        {
            List<UserView> model = new List<UserView>();
            
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_user").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                /*Debug.Write(data);*/
                /*model = JsonConvert.DeserializeObject<List<UserView>>(data);*/
                model = jv.listroot(model, data);
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
            String data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_user", content).Result;
            if (response.IsSuccessStatusCode)
            {
                String data2 = response.Content.ReadAsStringAsync().Result;
                Debug.Write(data2);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            UserView model = new UserView();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "edit_user&id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                //Debug.WriteLine(data);
                //Debug.WriteLine(root.uniroot);
                model = jv.uniroot(model, data);
            }
            return View(model);
        }

        [HttpPost]

        public IActionResult Edit(UserView model)
        {
            String data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "update_user&id=" + model.id, content).Result;
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
                model = jv.uniroot(model, data);
                /*Debug.Write(model.Name);*/

                string[] columnNames = new string[] { "id", "names", "email" };
                string csv = string.Empty;

                foreach (string column in columnNames)
                {
                    csv += column + ',';
                }
                csv += "\r\n";
                csv += model.id.ToString().Replace(",", ";") + ',';
                csv += model.names.Replace(",", ";") + ',';
                csv += model.email.Replace(",", ";") + ',';
                csv += model.password.Replace(",",";") + ',';

                csv += "\r\n";

                byte[] bytes = Encoding.ASCII.GetBytes(csv);
                return File(bytes, "text/csv", "user" + model.id + ".csv");
            }
            return View(model);
        }
        public IActionResult Import() { return View(); }
        [HttpPost]
        public IActionResult Import(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
            }

            List<UserView> model = new List<UserView>();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file.FileName;

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var user = csv.GetRecord<UserView>();
                    user.status = "0";
                    user.role = "1";
                    model.Add(user);
                }
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_user_csv", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data2 = response.Content.ReadAsStringAsync().Result;
                    Debug.Write(data2);
                    return RedirectToAction("Index");
                }
            }
            return Index();
        }
    }
}
