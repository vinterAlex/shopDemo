using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreManage.UI;



namespace StoreManage.UI.Controllers
{
    public class ProductsController : Controller
    {
        private StoreManagementEntities db = new StoreManagementEntities();




        // GET: Products
        public async Task<ActionResult> Index(string searchBy,string searchString)
        {



            if(searchBy == "Name")
            {
                return View(await db.Products1.Where(p=>p.Name.Contains(searchString)).ToListAsync());
            }
            else if(searchBy == "Price")
            {
                return View(await db.Products1.Where(p => p.Price.StartsWith(searchString)).ToListAsync());
            }
            else if(searchBy == "All")
            {
                return View(await db.Products1.ToListAsync());
            }

            //return View(await db.Products1.ToPagedList(pageNumber, pageSize));
            return View(await db.Products1.ToListAsync());


            //var productItems = from i in db.Products1
            //                   select i;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    productItems = productItems.Where(p => p.Name.Contains(searchString));
            //}


            //return View( productItems.ToList());
            //return View(await db.Products1.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products1.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,Name,Price,CreateDate,ModifyDate,InStock")] Products products)
        {
            if (ModelState.IsValid)
            {



                db.Products1.Add(products);

                products.CreateDate = System.DateTime.Now;
                products.ModifyDate = System.DateTime.Now;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products1.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,Name,Price,CreateDate,ModifyDate,InStock")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;

                products.CreateDate = System.DateTime.Now;
                products.ModifyDate = System.DateTime.Now;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products1.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Products products = await db.Products1.FindAsync(id);
            db.Products1.Remove(products);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
