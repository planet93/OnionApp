using System.Web;
using System.Web.Mvc;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using OnionApp.Services.Interfaces;
using OnionApp.Util;
using OnionApp.Infrastructure.Data;
using AutoMapper;
using OnionApp.Models;
using System.Collections.Generic;

namespace OnionApp.Controllers
{
    public class HomeController : Controller
    {
        IBookRepository repo;
        IOrder order;
        UnitOfWork unitOfWork;
        IRepository<User> repoUser;
        public HomeController(IBookRepository r, IOrder o)
        {
            repo = r;
            order = o;
            unitOfWork = new UnitOfWork();
            repoUser = new UserRepository();
        }
        public ActionResult Index()
        {
            //var books = unitOfWork.Books.GetAll();
            Mapper.Initialize(cfg => cfg.CreateMap<User, IndexUserViewModel>());
            var users = Mapper.Map<IEnumerable<User>, List<IndexUserViewModel>>(repoUser.GetAll());
            return View(users);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CreateUserViewModel, User>()
                .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
                .ForMember("Email", opt => opt.MapFrom(src => src.Login)));

                User user = Mapper.Map<CreateUserViewModel, User>(model);
                repoUser.Create(user);
                repoUser.Save();
                return RedirectToAction("Index");
            }
            return View(model);
            //if (ModelState.IsValid)
            //{
            //    unitOfWork.Books.Create(b);
            //    unitOfWork.Save();
            //    return RedirectToAction("Index");
            //}
            //return View(b);
        }

        public ActionResult Edit (int id)
        {
            Book b = unitOfWork.Books.Get(id);
            if(b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(Book b)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Books.Update(b);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public ActionResult Delete(int id)
        {
            unitOfWork.Books.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Buy(int id)
        {
            Book book = repo.GetBook(id);
            order.MakeOrder(book);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}