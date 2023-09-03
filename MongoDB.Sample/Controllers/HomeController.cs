using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Sample.DAL;
using MongoDB.Sample.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.Sample.Controllers
{
    public class HomeController : Controller
    {

       
        ShipperDAL shipperDal { get; set; }
        public HomeController(ShipperDAL shipper)
        {
           
            this.shipperDal = shipper;
        }
        public IActionResult Index()
        {
            var shippers = shipperDal.GetAll();
            return View(shippers);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Shipper shipper)
        {
            shipperDal.Add(shipper);
            return RedirectToAction("Index");
        }

        public ActionResult Update(string id)
        {
            var shipper = shipperDal.GetById(id);
            return View(shipper);
        }

        [HttpPost]
        public ActionResult Update(string id, Shipper shipper)
        {
            var item = shipperDal.GetById(id);
            item.CompanyName = shipper.CompanyName;
            item.Phone= shipper.Phone;
            shipperDal.Update(id, item);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var shipper1 = shipperDal.GetById(id);
            return View(shipper1);
        }

        [HttpPost]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            shipperDal.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
