using InnowisePilotWebClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InnowisePilotWebClient.Controllers
{
    public class FridgeController : Controller
    {
        string BaseUrl = "https://localhost:5001/";
        // GET: FridgeController
        public async Task<ActionResult> Index()
        {
            List<Fridge> FridgeInfo = new List<Fridge>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage responseMessage = await client.GetAsync("api/Fridge");

            if (responseMessage.IsSuccessStatusCode)
            {
                var FridgeResponse = responseMessage.Content.ReadAsStringAsync().Result;
                FridgeInfo = JsonConvert.DeserializeObject<List<Fridge>>(FridgeResponse);
            }
            return View(FridgeInfo);
        }

        // GET: FridgeController/ToEdit/5
        public async Task<ActionResult> Details(int id)
        {
            Fridge FridgeDetails = new Fridge();

            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync("api/Fridge/"+id);
            if (Res.IsSuccessStatusCode)
            {
                var record = Res.Content.ReadAsStringAsync().Result;
                FridgeDetails = JsonConvert.DeserializeObject<Fridge>(record);
            }
            return View(FridgeDetails);
        }

        // GET: FridgeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FridgeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FridgeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Fridge FridgeToEdit = new Fridge();
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync("api/Fridge/" + id);
            if (Res.IsSuccessStatusCode)
            {
                var record = Res.Content.ReadAsStringAsync().Result;
                FridgeToEdit = JsonConvert.DeserializeObject<Fridge>(record);
            }
            return View(FridgeToEdit);
            
        }

        // POST: FridgeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Fridge FridgeToEdit)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            FridgeToEdit.Model = null;
            HttpResponseMessage Res = await client.PutAsJsonAsync<Fridge>("api/Fridge/" + id, FridgeToEdit);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FridgeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FridgeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
