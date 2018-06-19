using WeightLogging.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System;
using WeightLogging;
using PagedList;
using WeightLogging.ViewModel;

namespace WeightLogging.Controllers
{
    public class WeightListController : Controller
    {
        private weightlogEntities db = new weightlogEntities();
        private Util util = new Util();

        // GET: WeightList/Index
        public ActionResult Index(int PageNumber = 1)
        {
            var weight_list = db.weight_list.AsQueryable();
            weight_list = weight_list.OrderByDescending(w => w.record_date);

            ViewBag.weight_list = weight_list.ToPagedList(PageNumber, 3);
            return View();
        }

        // GET: WeightList/Details/2018-06-19
        public ActionResult Details(string id)
        {
            var weight_record = GetWeightRecord(id);
            return View(weight_record);
        }

        // GET: WeightList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeightList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(weight_list weight_list)
        {
            if (ModelState.IsValid)
            {
                if (util.SaveChangesToDB(this, db, new weight_list[] { weight_list }))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(weight_list);
        }

        // GET: WeightList/Edit/2018-06-19
        public ActionResult Edit(string id)
        {
            /* The edit screen will be pre-populated with the same information as when viewing the details of a single record */
            return Details(id);
        }

        // POST: WeightList/Edit/2018-06-19
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(weight_list weight_list, string id)
        {
            var weight_list_original = GetWeightRecord(id);

            if (ModelState.IsValid)
            {
                weight_list_original.record_date = weight_list.record_date;
                weight_list_original.max_weight = weight_list.max_weight;
                weight_list_original.min_weight = weight_list.min_weight;

                if (util.SaveChangesToDB(this, db, new weight_list[] { weight_list_original }, true))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(weight_list_original);
        }

        // GET: WeightList/Delete/2018-06-19
        public ActionResult Delete(string id)
        {
            var weight_list_original = GetWeightRecord(id, db);

            db.weight_list.Remove(weight_list_original);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public static weight_list GetWeightRecord(string dateString, weightlogEntities db = null)
        {
            DateTime record_date = DateTime.Parse(dateString);

            if (db == null)
            {
                db = new weightlogEntities();
            }
            var weight_list = db.weight_list.AsQueryable().ToList()
                                .Where(wl => wl.record_date == record_date);

            weight_list weight_list_original = null;

            if (weight_list != null && weight_list.Count() > 0)
            {
                weight_list_original = weight_list.ToArray()[0];
            }

            return weight_list_original;
        }
    }
}
