using ExpertAI.RequestAllocator.Data;
using ExpertAI.RequestAllocator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExpertAI.RequestAllocator.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult MakeRequest()
        {
            MakeRequestViewModel model = new MakeRequestViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MakeRequest(MakeRequestFormModel Form)
        {
            if(!ModelState.IsValid)
            {
                MakeRequestViewModel model = new MakeRequestViewModel();
                model.Form = Form;
                return View(model);
            }
            string token = string.Empty;
            var urlToken = "https://developer.expert.ai/oauth2/token";
            var json = JsonConvert.SerializeObject(new { username = ExpertUser, password = ExpertPassword });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
                var response = client.PostAsync(urlToken, data).Result;

                token = response.Content.ReadAsStringAsync().Result;
            }

            var urlAnalysis = "https://nlapi.expert.ai/v2/analyze/standard/en";
            var jsonAnalysis = JsonConvert.SerializeObject(new { document = new { text = Form.Request } });
            var dataAnalysis = new StringContent(jsonAnalysis, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer "+token);
                var response = client.PostAsync(urlAnalysis, dataAnalysis).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                var respuesta = JsonConvert.DeserializeObject<ExpertAnalysisResponse>(result);
                if(respuesta != null && respuesta.success)
                {
                    Data.Request dataRequest = new Request
                    {
                         Area = Form.Area, CreatedAt = DateTime.Now, RequestText = respuesta.data.content, Sentiment = respuesta.data.sentiment.overall
                    };

                    if(respuesta.data.topics != null && respuesta.data.topics.Any())
                    {
                        foreach(var topic in respuesta.data.topics)
                        {
                            dataRequest.TopicRequests.Add(new TopicRequest { Label = topic.label, Score = topic.score, Winner = topic.winner });
                        }
                    }
                    db.Requests.Add(dataRequest);
                }
                db.SaveChanges();
            }


            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            return View();
        }

    }
}