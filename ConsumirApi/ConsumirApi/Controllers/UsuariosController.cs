using ConsumirApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsumirApi.Controllers
{
    public class UsuariosController : Controller
    {
        string ApiUrl = "http://localhost:52407/";
        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            List<Usuarios> EmpInfo = new List<Usuarios>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Usuarios/");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Usuarios>>(empResponse);
                }
                return View(EmpInfo);
            }
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Usuarios usuarios)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52407/api/Usuarios");
                var postTask = client.PostAsJsonAsync<Usuarios>("Usuarios",usuarios);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, problemas al intentar insertat datos.");
            return View(usuarios);
        }
        public ActionResult Edit(int id)
        {
            Usuarios usuarios = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52407/");
                var responseTask=  client.GetAsync("api/Usuarios/" + id.ToString());
                responseTask.Wait();
                
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Usuarios>();
                    readTask.Wait();
                    usuarios = readTask.Result;
                }
            }
            return View(usuarios);

        }
        [HttpPost]
        public ActionResult Edit(Usuarios usuarios)
        {
            using (var client =new HttpClient()) {
                client.BaseAddress = new Uri("http://localhost:52407/");
                //HttpClient POST
                var putTask = client.PutAsJsonAsync($"api/Usuarios/{usuarios.Id}", usuarios);
                putTask.Wait();
                var result = putTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(usuarios);
        }
    }
}