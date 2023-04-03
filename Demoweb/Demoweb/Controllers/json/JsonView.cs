using Demoweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demoweb.Controllers.json
{
    public class JsonView
    {
        public List<UserView> listroot(List<UserView> model, String data)
        {
            model = new List<UserView>();
            RootObject root = JsonConvert.DeserializeObject<RootObject>(data);
            model = root.result;
            return model;
        }
        public UserView uniroot(UserView user, String data)
        {
            Root root = JsonConvert.DeserializeObject<Root>(data);
            user = root.result;
            return user;
        }
        public Login LoginDetails(String data)
        {
            Login loginDetails = JsonConvert.DeserializeObject<Login>(data);
            return loginDetails;
        }
        
    }
    public class RootObject
    {
        public List<UserView> result { get; set; }
    }
    public class Root
    {
        public UserView result { get; set; }
    }
    public class Login
    {
        public bool success { get; set; }
        public UserView user { get; set; }
        public Student student { get; set; }
    }
    public class Student
    {
        public string id { get; set; }
        public string session_id { get; set; }
        public string user_id { get; set; }
        public string department_id { get; set; }
        public string surname { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string grand_father_name { get; set; }
        public string enrollment { get; set; }
        public string gender { get; set; }
        public string contact { get; set; }
        public string alt_contact { get; set; }
        public string parent_contact { get; set; }
        public string dob { get; set; }
        public string lang_eng { get; set; }
        public string lang_hindi { get; set; }
        public string lang_guj { get; set; }
        public string lang_marathi { get; set; }
        public string lang_other { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string permanent_address { get; set; }
        public string permanent_pincode { get; set; }
        public string permanent_city { get; set; }
        public string permanent_state { get; set; }
        public string blood_group { get; set; }
        public string adhaar { get; set; }
        public string pan { get; set; }
        public string driving { get; set; }
        public string ten_school { get; set; }
        public string ten_passyear { get; set; }
        public string ten_schooladdress { get; set; }
        public string ten_schoolpincode { get; set; }
        public string ten_schoolcity { get; set; }
        public string ten_board { get; set; }
        public string ten_score { get; set; }
        public string ten_scoreoutof { get; set; }
        public string ten_gapno { get; set; }
        public string ten_gapyears { get; set; }
        public string ten_admissionquota { get; set; }
        public string twelve_school { get; set; }
        public string twelve_passyear { get; set; }
        public string twelve_schooladdress { get; set; }
        public string twelve_schoolpincode { get; set; }
        public string twelve_schoolcity { get; set; }
        public string twelve_board { get; set; }
        public string twelve_score { get; set; }
        public string twelve_scoreoutof { get; set; }
        public string twelve_gapno { get; set; }
        public string twelve_gapyears { get; set; }
        public string twelve_admissionquota { get; set; }
        public string twelve_specialization { get; set; }
        public string ug_degree { get; set; }
        public string ug_college { get; set; }
        public string ug_passyear { get; set; }
        public string ug_collegeaddress { get; set; }
        public string ug_collegecity { get; set; }
        public string ug_collegepincode { get; set; }
        public string ug_university { get; set; }
        public string ug_score { get; set; }
        public string ug_scoreoutof { get; set; }
        public string ug_gapno { get; set; }
        public string ug_gapyears { get; set; }
        public string ug_admissionquota { get; set; }
        public string pg_degree { get; set; }
        public string pg_college { get; set; }
        public string pg_passyear { get; set; }
        public string pg_collegeaddress { get; set; }
        public string pg_collegecity { get; set; }
        public string pg_collegepincode { get; set; }
        public string pg_university { get; set; }
        public string pg_score { get; set; }
        public string pg_scoreoutof { get; set; }
        public string pg_gapno { get; set; }
        public string pg_gapyears { get; set; }
        public string pg_admissionquota { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
