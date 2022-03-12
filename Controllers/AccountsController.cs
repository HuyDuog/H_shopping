using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using HShopping.Extensions;
using HShopping.Helpers;
using HShopping.Models;
using HShopping.ModelView;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HShopping.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {

        private readonly dbHshoppingContext _context;
        public INotyfService _notyfService { get; }
        public AccountsController(dbHshoppingContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                if(khachhang != null)
                {
                    var lsdonhang = _context.Orders.AsNoTracking().Include(x => x.TransactStatus).Where(x => x.CustomerId == khachhang.CustomerId).OrderByDescending(x => x.OrderDate).ToList();
                    ViewBag.lsDonhang = lsdonhang;
                    return View(khachhang);
                }    
         
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ki.html",Name ="Dangky")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ki.html", Name = "Dangky")]
        public async Task<IActionResult> Register(AccountViewModel taikhoan)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Customers customer = new Customers
                    {
                        FullName = taikhoan.FullName,
                        Phone = taikhoan.Phone.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = taikhoan.Password.ToMD5(),
                        Active = true,
                        CreateDate = DateTime.Now
                    };
                    try
                    {
                        _context.Add(customer);
                        await _context.SaveChangesAsync();
                        HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
                        var taikhoanid = HttpContext.Session.GetString("CustomerId");
                        var claim = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, customer.FullName),
                            new Claim("CustomerId",customer.CustomerId.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claim, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    catch
                    {
                        return RedirectToAction("Register", "Accounts");
                    }
                } 
                else
                {
                    return View(taikhoan);
                }    
             
            }
            catch
            {
                return View(taikhoan);

            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string phone)
        {
            try
            {
                var customer = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == phone.ToLower());
                if(customer !=null)
                    return Json(data: "So dien thoai : " + phone + "da duoc su dung");
                    return Json(data: true);
                
            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string email)
        {
            try
            {
                var customer = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
                if (customer != null)
                    return Json(data: "Email : " + email + "da duoc su dung");
                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Dangnhap")]
        public IActionResult Login(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {

                return RedirectToAction("Dashboard", "Accounts");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Dangnhap")]
        public async Task<IActionResult> Login (LoginViewModel model , string returnUrl = null)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(model.UserName);
                    if (!isEmail) return View(model);

                    var customer = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == model.UserName);
                    if (customer == null)
                        return RedirectToAction("Register");
                    string pass = model.Password.ToMD5();
                    if(customer.Password != pass)
                    {
                        _notyfService.Success("Sai thong tin dang nhap");
                        return View();
                    }
                    if(customer.Active == false)
                    {
                        return RedirectToAction("Thong bao", "Accounts");
                    }

                    HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomerId");
                    var claim = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, customer.FullName),
                            new Claim("CustomerId",customer.CustomerId.ToString())
                        };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claim, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Login Success");
                    return RedirectToAction("Dashboard", "Accounts");

                }
            }
            catch
            {
                return RedirectToAction("Register", "Accounts");
            }
            return View(model);
        }
        [HttpGet]
        [Route("logout.html", Name ="Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult ChangePassword (ChangePassword model )
        {
            try
            {
                var taikhoanid = HttpContext.Session.GetString("CustomerId");
                if(taikhoanid == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _context.Customers.Find(Convert.ToInt32(taikhoanid));
                    if (taikhoan == null) return RedirectToAction("Login", "Accounts");
                    var pass = model.PasswordNow.Trim().ToMD5();
                    if (pass == taikhoan.Password)
                    {
                        string passnew = (model.Password.Trim().ToMD5());
                        taikhoan.Password = passnew;
                        _context.Update(taikhoan);

                        _context.SaveChanges();
                        _notyfService.Success("Change Password Success ");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Dashboard", "Accounts");
            }

            return RedirectToAction("Dashboard", "Accounts");
        }
    }
}
