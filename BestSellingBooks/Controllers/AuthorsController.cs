using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using BestSellingBooks.DAL;
using BestSellingBooks.ViewModels;
using System.Linq.Dynamic;using AutoMapper;


namespace BestSellingBooks.Controllers
{
    public class AuthorsController : Controller
    {
        private BooksContext db = new BooksContext();
        // GET: Authors
        public ActionResult Index([Form] QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            var authors = db.Authors.
            OrderBy(queryOptions.Sort).
            Skip(start).
            Take(queryOptions.PageSize);
            queryOptions.TotalPages =
            (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);
            ViewBag.QueryOptions = queryOptions;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();    
            });
            var mapper = configuration.CreateMapper();
            //AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            return View(mapper.Map<List<Author>,
            List<AuthorViewModel>>(authors.ToList()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Biography")]
        AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Author, AuthorViewModel>();
                });
                var mapper = configuration.CreateMapper();
                db.Authors.Add(mapper.Map<AuthorViewModel, Author>(author));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }
        // GET: Authors/Create
        public ActionResult Create()
        {
            return View("Form", new AuthorViewModel());
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });
            var mapper = configuration.CreateMapper();

            return View("Form", mapper.Map<Author, AuthorViewModel>(author));
        }
            



    // POST: Authors/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Author, AuthorViewModel>();
                });
                var mapper = configuration.CreateMapper();
                db.Entry(mapper.Map<AuthorViewModel, Author>(author)).State
                = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Form", author);
        }

        // GET: Authors/Detail/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });
            var mapper = configuration.CreateMapper();
            return View(mapper.Map<Author, AuthorViewModel>(author));
        }
        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });
            var mapper = configuration.CreateMapper();
            return View(mapper.Map<Author, AuthorViewModel>(author));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
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
