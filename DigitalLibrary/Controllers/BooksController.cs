using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookGenreRepository _bookGenreRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReserveRepository _reserveRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public BooksController(IAuthorRepository authorRepository,
            IBookGenreRepository bookGenreRepository,
            IBookRepository bookRepository,
            IReserveRepository reserveRepository,
            IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            _authorRepository = authorRepository;
            _bookGenreRepository = bookGenreRepository;
            _bookRepository = bookRepository;
            _reserveRepository = reserveRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAll();

            return View(books);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            await  CreateCombos();
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid) return View(book);

            await _bookRepository.Add(book);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            await CreateCombos();
            return View(book);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid) return View(book);

            await _bookRepository.Update(book);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var book = await _bookRepository.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Reserve")]
        public async Task<IActionResult> ReserveIndex()
        {
            var books = await _bookRepository.GetAll();

            if (!User.IsInRole("Administrator"))
            {
                var reserveds = await _reserveRepository.GetWhere(x => !x.Returned);
                var avalableBooks = books.Where(b => !reserveds.Any(r => r.BookId == b.Id)).ToList();
                books = avalableBooks;
            }

            return View(books);
        }

        [HttpGet]
        [Route("Reserve/{id:guid}")]
        public async Task<IActionResult> Reserve(Guid id)
        {

            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [Route("Reserve/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveConfirmed(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var appUser = await _userRepository.FirstOrDefault(u => u.Email == user.Email);

            var reserve = new Reserve
            {
                UserId = appUser.Id,
                BookId = book.Id,
                Returned = false,
            };

            await _reserveRepository.Add(reserve);

            return RedirectToAction(nameof(ReserveIndex));
        }

        [HttpGet]
        [Route("Return")]
        public async Task<IActionResult> ReturnIndex()
        {
            var books = await _bookRepository.GetAll();

            if (!User.IsInRole("Administrator"))
            {
                var user = await _userManager.GetUserAsync(User);
                var appUser = await _userRepository.FirstOrDefault(u => u.Email == user.Email);
                var reserveds = await _reserveRepository.GetWhere(x => !x.Returned && x.UserId == appUser.Id);
                var userBooks = books.Where(b => reserveds.Any(r => r.BookId == b.Id)).ToList();
                books = userBooks;
            }

            return View(books);
        }

        [HttpGet]
        [Route("Return/{id:guid}")]
        public async Task<IActionResult> Return(Guid id)
        {

            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [Route("Return/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }
            
            var user = await _userManager.GetUserAsync(User);
            var appUser = await _userRepository.FirstOrDefault(u => u.Email == user.Email);

            var reserved = await _reserveRepository.GetWhere(r => r.BookId == book.Id && r.UserId == appUser.Id && !r.Returned);
            var update = reserved.FirstOrDefault();
            if (update == null)
            {
                return NotFound();
            }

            update.Returned = true;

            await _reserveRepository.Update(update);

            return RedirectToAction(nameof(ReturnIndex));
        }

        private async Task CreateCombos()
        {
            ViewBag.BookGenres = new SelectList(await _bookGenreRepository.GetAll(),
            "Id", "Name");
            ViewBag.Authors = new SelectList(await _authorRepository.GetAll(),
            "Id", "Name");
        }

    }
}
