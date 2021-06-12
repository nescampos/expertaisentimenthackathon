using ExpertAI.RequestAllocator.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertAI.RequestAllocator.Controllers
{
    public class BaseController : Controller
    {

        protected string ExpertUser = ConfigurationManager.AppSettings["ExpertUser"];
        protected string ExpertPassword = ConfigurationManager.AppSettings["ExpertPassword"];
        protected expertaiEntities db = new expertaiEntities();

    }
}