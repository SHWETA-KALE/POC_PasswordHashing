using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HasingBcryptLogin.Data;
using HasingBcryptLogin.Models;
using NHibernate;

namespace HasingBcryptLogin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISessionFactory _sessionFactory;

        public AccountController()
        {
            _sessionFactory = NHibernateHelper.CreateSession().SessionFactory;
        }


        // GET: Account
        //shows the login page
        public ActionResult Index(bool isCreatingUser = false)
        {
            var model = new AccountViewModel
            {
                IsCreatingUser = isCreatingUser
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AccountViewModel model)
        {
            if (ModelState.IsValid) {
                using (var session = NHibernateHelper.CreateSession())
                {
                    if (model.IsCreatingUser)
                    {
                        using (var transaction = session.BeginTransaction()) {
                            var user = new User
                            {
                                Username = model.Username,
                                PasswordHash = PasswordHelper.HashPassword(model.Password)
                            };
                            session.Save(user);
                            transaction.Commit();

                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //handle login
                        var user = session.Query<User>().FirstOrDefault(u => u.Username == model.Username);
                        if (user != null && PasswordHelper.VerifyPassword(model.Password, user.PasswordHash))
                        {
                            ViewBag.WelcomeMessage = "Welcome, " + model.Username;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Username and/or password is incorrect.";
                        }
                    }
                }
            }
            return View(model);
        }
    }
}

//admin username
//admin password

//sk username
//sk password
