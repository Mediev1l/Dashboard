using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBoard.Models.BoardModels;
using MyBoard.Models.Identity;
using MyBoard.Repository.IRepository;

namespace MyBoard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(IEnumerable<Board> board)
        {
            //var user = await _userManager.FindByNameAsync("test1234");
            //var result = await _signInManager.PasswordSignInAsync(user , "test1234", false, false);

            ViewData["user"] = true;//User.Identity.IsAuthenticated;
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Board board)
        {
            if(!ModelState.IsValid)
                return View();

            var newBoard = new Board()
            {
                Name = board.Name                
            };

            _unitOfWork.Board.Add(newBoard);
            _unitOfWork.Save();


            var newUserBoard = new UserBoard()
            {
                BoardId = newBoard.Id,
                UserId = Convert.ToInt32(_userManager.GetUserId(HttpContext.User))
            };

            _unitOfWork.UserBoard.Add(newUserBoard);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
