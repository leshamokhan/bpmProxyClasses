using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bpm.Models;
using bpm.BPMonlineServiceReference;

namespace bpm.Controllers
{
    public class HomeController : Controller
    {
        private IEnumerable<Contact> contacts;


        public HomeController()
        {
            if (LoginClass.TryLogin())
            {
                contacts = ProxyService.GetOdataCollectioByLinqWcfExample();
            }
        }
        

        public ActionResult Index()
        {           
            return View(contacts);
        }
               

        public ViewResult Create()
        {
            return View(new Contact());
        }


        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                HTTPService.CreateBpmEntityByOdataWcfExample(contact);
                return RedirectToAction("Index");
            }
            else
            {
                return View(contact);
            }
           
        }


        public ViewResult Edit(Guid contactId)
        {
            Contact contact = contacts.FirstOrDefault(c => c.Id == contactId);
            return View(contact);            
        }


        public ActionResult Save(Contact contact)
        {
            if (ModelState.IsValid)
            {
                HTTPService.UpdateBpmEntityByOdatetWcfExample(contact);                
            }
            return RedirectToAction("Index");
        }
        

        public ActionResult Delete(Guid contactId)
        {
            if (ModelState.IsValid)
            {
                HTTPService.DeleteBpmEntityByOdataWcfExample(contactId);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}