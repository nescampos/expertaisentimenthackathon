using ExpertAI.RequestAllocator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpertAI.RequestAllocator.Models
{
    public class IndexMonitorViewModel
    {
        public IEnumerable<Request> Requests { get; set; }
        public IEnumerable<IGrouping<string, Request>> Areas { get; set; }
        public string Sentiment { get; set; }
        public IEnumerable<IGrouping<string, TopicRequest>> Topics { get; internal set; }

        public int Qty { get; set; }
    }
}