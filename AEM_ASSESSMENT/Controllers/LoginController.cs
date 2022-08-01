using AEM_ASSESSMENT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AEM_ASSESSMENT.Controllers
{
    
    [ApiController]
    [Route("[controller]")]

    public class LoginController : Controller
    {
        private readonly AEM_TECHNICAL_ASSESMENTContext _dbcontext;
        public LoginController(AEM_TECHNICAL_ASSESMENTContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpPost("login")]
        public async Task<string> login()

        {
            var url = "http://test-demo.aemenersol.com/api/Account/Login";


            using var client = new HttpClient();
            var login = new Login();
            login.UserName = "user@aemenersol.com";
            login.Password = "Test@123";
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(response.StatusCode);
            return result;

        }

        [HttpGet("GetPlatformWellActual")]
        public async Task<List<Platform>> GetPlatformWellActual() 
        {


                var TARGETURL = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual";

            HttpClientHandler handler = new HttpClientHandler();

                Console.WriteLine("GET: + " + TARGETURL);
                // ... Use HttpClient.            
                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGFlbWVuZXJzb2wuY29tICIsImp0aSI6ImI0ZTUwOTM1LTQ5NjgtNGE3MS05MjkwLTU4Y2VmNTdkZTcxOCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMzMxOGU3MTAtOTMwMy00OGZkLTgzYzUtZmJjYTk1NDExMWVmIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY2MTQ5ODY4OCwiaXNzIjoiaHR0cDovL3Rlc3QtZGVtby5hZW1lbmVyc29sLmNvbSIsImF1ZCI6Imh0dHA6Ly90ZXN0LWRlbW8uYWVtZW5lcnNvbC5jb20ifQ.kDKSiGLeoXDZrY7H820YT7FKa9UMCdYU_l9xr81JT8I");

                HttpResponseMessage response = await client.GetAsync(TARGETURL);
                HttpContent content = response.Content;

                // ... Check Status Code                                
                Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            
            // ... Read the string.
            string result = await content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<List<Platform>>(result);

            var newlist = new List<Platform>();

            if (json != null)
            {


                foreach (var data in json)
                {
                    var dataexist = _dbcontext.Platforms.Where(x => x.Id == data.Id).FirstOrDefault();
                    if (dataexist != null)
                    {
                        dataexist.UniqueName = data.UniqueName;
                        dataexist.Latitude = data.Latitude;
                        dataexist.Longitude = data.Longitude;
                        dataexist.CreatedAt = data.CreatedAt;
                        dataexist.UpdatedAt = data.UpdatedAt;
                        _dbcontext.Platforms.Update(dataexist);
                        foreach (var t in dataexist.Well)

                        {
                            var wellexist = _dbcontext.Well.Where(x => x.Id == t.Id).FirstOrDefault();
                            wellexist.UniqueName = t.UniqueName;
                            wellexist.Latitude = t.Latitude;
                            wellexist.Longitude = t.Longitude;
                            wellexist.CreatedAt = t.CreatedAt;
                            wellexist.UpdatedAt = t.UpdatedAt;
                            _dbcontext.Well.Update(wellexist);

                        }
                        //newlist.Add(dataexist);


                    }
                    else
                    {
                        var platform = new Platform();
                        platform.Id = data.Id;
                        platform.UniqueName = data.UniqueName;
                        platform.Latitude = data.Latitude;
                        platform.Longitude = data.Longitude;
                        platform.CreatedAt = data.CreatedAt;
                        platform.UpdatedAt = data.UpdatedAt;
                        //platform.Well = data.Well;
                        _dbcontext.Platforms.Add(platform);
                        foreach (var x in data.Well)
                        {
                            var well = new Well();
                            well.Id = x.Id;
                            well.PlatformId = x.PlatformId;
                            well.UniqueName = x.UniqueName;
                            well.Latitude = x.Latitude;
                            well.Longitude = x.Longitude;
                            well.CreatedAt = x.CreatedAt;
                            well.UpdatedAt = x.CreatedAt;
                            _dbcontext.Well.Add(well);

                        }
                        //newlist.Add(platform);
                    }



                }

                _dbcontext.SaveChanges();
            }
             newlist = _dbcontext.Platforms.Include(x=>x.Well).ToList();
            
            
            return newlist;
            
        }
        [HttpGet("GetPlatformWellActualDummy")]
        public async Task<List<Platform>> GetPlatformWellActualDummy()
        {


            var TARGETURL = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActualDummy";

            HttpClientHandler handler = new HttpClientHandler();

            Console.WriteLine("GET: + " + TARGETURL);
                     
            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGFlbWVuZXJzb2wuY29tICIsImp0aSI6ImI0ZTUwOTM1LTQ5NjgtNGE3MS05MjkwLTU4Y2VmNTdkZTcxOCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMzMxOGU3MTAtOTMwMy00OGZkLTgzYzUtZmJjYTk1NDExMWVmIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY2MTQ5ODY4OCwiaXNzIjoiaHR0cDovL3Rlc3QtZGVtby5hZW1lbmVyc29sLmNvbSIsImF1ZCI6Imh0dHA6Ly90ZXN0LWRlbW8uYWVtZW5lcnNvbC5jb20ifQ.kDKSiGLeoXDZrY7H820YT7FKa9UMCdYU_l9xr81JT8I");

            HttpResponseMessage response = await client.GetAsync(TARGETURL);
            HttpContent content = response.Content;

            // ... Check Status Code                                
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);


            string result = await content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<List<Platform>>(result);

            if (json == null)
            {
                throw new Exception();
            }


            
            if (result != null &&
                result.Length >= 50)
            {
                Console.WriteLine(result.Substring(0, 50) + "...");
            }
            var newlist = new List<Platform>();

            foreach (var data in json)
            {
                var dataexist = _dbcontext.Platforms.Where(x => x.Id == data.Id).FirstOrDefault();
                if (dataexist != null)
                {
                    dataexist.UniqueName = data.UniqueName;
                    dataexist.Latitude = data.Latitude;
                    dataexist.Longitude = data.Longitude;
                    _dbcontext.Platforms.Update(dataexist);
                    foreach (var t in dataexist.Well)

                    {
                        var wellexist = _dbcontext.Well.Where(x => x.Id == t.Id).FirstOrDefault();
                        wellexist.UniqueName = t.UniqueName;
                        wellexist.Latitude = t.Latitude;
                        wellexist.Longitude = t.Longitude; 
                        _dbcontext.Well.Update(wellexist);

                    }

                }
                else
                {
                    var platform = new Platform();
                    platform.Id = data.Id;
                    platform.UniqueName = data.UniqueName;
                    platform.Latitude = data.Latitude;
                    platform.Longitude = data.Longitude;
                    //platform.Well = data.Well;
                    _dbcontext.Platforms.Add(platform);
                    foreach (var x in data.Well)
                    {
                        var well = new Well();
                        well.Id = x.Id;
                        well.PlatformId = x.PlatformId;
                        well.UniqueName = x.UniqueName;
                        well.Latitude = x.Latitude;
                        well.Longitude = x.Longitude;
                        _dbcontext.Well.Add(well);

                    }
                }



            }

            _dbcontext.SaveChanges();


            return newlist;

        }




    }
}


