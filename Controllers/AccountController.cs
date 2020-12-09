using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBoard.Models.Email;
using MyBoard.Models.Identity;
using MyBoard.Models.ViewModels;
using MyBoard.Repository.IRepository;

namespace MyBoard.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Login"] = true;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
            {
            ViewData["Login"] = true;

            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(loginUser.Login.Username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginUser.Login.Password, false, false);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.Message = "Wrong username or password";
                }
            }
            else
            {
                ViewBag.Message = "Wrong username or password";
            }

            return View();
            
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Login"] = false;
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registerUser)
        {
            ViewData["Login"] = false;

            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            var user = await _userManager.FindByNameAsync(registerUser.Register.Username);

            if(user == null)
            {
                user = new AppUser()
                {
                    UserName = registerUser.Register.Username,
                    Email = registerUser.Register.Email
                };

                var result = await _userManager.CreateAsync(user, registerUser.Register.Password);

                if(result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = user.Id, code}, Request.Scheme, Request.Host.ToString());


                    await _emailService.SendAsync(new EmailMessage() {
                        FromAddress = new EmailAddress()
                        {
                            Address = "dailyweatherinfo@gmail.com",
                            Name = "Daily"
                        },
                        ToAddress = new EmailAddress()
                        {
                            Address = "bartek5903@gmail.com",
                            Name = "Bartek"
                        },
                        Subject = "Register",
                        Body = $"{link}"

                    }) ;


                    return View("EmailVerification", user);

                }

            }
            else
            {
                ViewBag.Message = "User already registered";
            }

            return View("Login");
        }


        [NonAction]
        public IActionResult EmailVerification(AppUser user) => View(user);

       
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null) return BadRequest();
            
            var result = await _userManager.ConfirmEmailAsync(user, code);
            
            if(result.Succeeded)
            {
                return View();
            }
            
            return BadRequest();
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAdmin()
        {
            var user = await _userManager.GetUserAsync(User);


            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Admin", "true"));

            

            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> removeAdmin()
        {
            var user = await _userManager.GetUserAsync(User);


            await _userManager.ReplaceClaimAsync(user, new System.Security.Claims.Claim("Admin", "true"), new System.Security.Claims.Claim("Admin", "false"));



            return RedirectToAction("Index", "Dashboard");
        }

    }
}
