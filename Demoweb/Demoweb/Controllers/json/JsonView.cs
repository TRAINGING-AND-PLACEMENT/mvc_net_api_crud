using Demoweb.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
    public class RootObject
    {
        public List<UserView> result { get; set; }
    }
    public class Root
    {
        public UserView result { get; set; }
    }
}
