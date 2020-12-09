using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBoard.Models.Identity;
using MyBoard.Repository.IRepository;

namespace MyBoard.Controllers
{
    public class MyBoardsController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public MyBoardsController(SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            var boards = _unitOfWork.UserBoard.Include(b => b.Board, c => c.User).Where(u => u.UserId == userId).Select(s => s.Board);
            return View(boards);
        }
    }
}
