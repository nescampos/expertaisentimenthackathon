using ExpertAI.RequestAllocator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertAI.RequestAllocator.Controllers
{
    [Authorize]
    public class MonitorController : BaseController
    {
        // GET: Monitor
        public ActionResult Index(string area, string topic)
        {
            IndexMonitorViewModel model = new IndexMonitorViewModel();

            IEnumerable<Data.Request> requestList = db.Requests.OrderByDescending(x => x.CreatedAt);

            if(!string.IsNullOrWhiteSpace(area))
            {
                requestList = requestList.Where(x => x.Area == area);
            }

            if (!string.IsNullOrWhiteSpace(topic))
            {
                requestList = requestList.Where(x => x.TopicRequests.Any(y => y.Label == topic));
            }

            var areas = db.Requests.GroupBy(x => x.Area);
            var topics = db.TopicRequests.GroupBy(x => x.Label).OrderByDescending(x => x.Count()).Take(10);
            var sentiment = requestList.Any()? (requestList.Average(x => x.Sentiment) > 0 ? "Positive" : "Negative"): "Neutral";

            model.Requests = requestList;
            model.Areas = areas;
            model.Sentiment = sentiment;
            model.Topics = topics;
            model.Qty = db.Requests.Count();
            return View(model);
        }
    }
}