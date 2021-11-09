using DSCC.CW_1._00008056MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC.CW_1._00008056MVC.Controllers
{
    public class CDAlbumController : Controller
    {

        //Coontroller for passing the values that come from ASP.NET CORE Microservice API to VIEW
        //BaseURL address from which the DB values will be passed in a JSON format 
        //After deployment should be changed to hosting url
        string Baseurl = "http://dscccw100008056-dev.us-east-1.elasticbeanstalk.com/";
        private string urlStarter = "api/CDAlbum";
        // GET: ALbum
        public async Task<ActionResult> Index()
        {
            //storing a list of album objects inside a variable  
            var albums = new List<Album>(); //declaring and assiging a variable values passed from API
            string body = null; //declaring a variable that will further be used for storing the response result 

            var client = new HttpClient();
            var response = await client.GetAsync(Baseurl + urlStarter);//specifying the url from which the response should to our request (to get the list of albums) should come
            if (response.IsSuccessStatusCode)
            {
                body = await response.Content.ReadAsStringAsync();  
                albums = JsonConvert.DeserializeObject<List<Album>>(body); //converting JSON values retrieved from API into a list form 
            }

            return View(albums);//returning a list to View (Index) 
        }

        // GET: CDAlbumController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Album album = await GetAlbumById(id);

            return View(album);
        }

        // GET: CDAlbumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CDAlbumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //to prevent cross-site requests
        public async Task<ActionResult> Create(Album alb)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(alb);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Baseurl + urlStarter, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CDAlbumController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Album item = await GetAlbumById(id);
            return View(item);
        }

        // POST: CDAlbumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Album alb)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(alb); ;
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(Baseurl + urlStarter + "/" + id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CDAlbumController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Album item = await GetAlbumById(id);
            return View(item);
        }

        // POST: CDAlbumController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Album alb)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.DeleteAsync(Baseurl + urlStarter + "/" + id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Method was seperately created to choose an Album by Id and to avoid code duplication since Create, Edit and Delete methods use it (not POST) 
        public async Task<Album> GetAlbumById(int id)
        {
            Album album = null;
            var client = new HttpClient();
            var response = await client.GetAsync(Baseurl + urlStarter + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                album = JsonConvert.DeserializeObject<Album>(result);
            }
            return album;
        }

    }
}

