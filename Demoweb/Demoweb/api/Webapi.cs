namespace Demoweb.api
{
    public class Webapi
    {
        Uri baseAddress = new Uri("http://192.168.229.181/api/all_api.php?what=");
        public System.Uri api()
        {
            return baseAddress;
        }
    }
}
