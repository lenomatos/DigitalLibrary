using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BookGenresController : Controller
    {
        private readonly IBookGenreRepository _bookGenreRepository;

        public BookGenresController(IBookGenreRepository bookGenreRepository)
        {
            _bookGenreRepository = bookGenreRepository;
        }
        public async Task<IActionResult> Index()
        {
            var booksGenre = await _bookGenreRepository.GetAll();
            return View(booksGenre);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var booksGenre = await _bookGenreRepository.GetById(id);

            if (booksGenre == null)
            {
                return NotFound();
            }

            return View(booksGenre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookGenre bookGenre)
        {
            if (!ModelState.IsValid) return View(bookGenre);

            await _bookGenreRepository.Add(bookGenre);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var booksGenre = await _bookGenreRepository.GetById(id);

            if (booksGenre == null)
            {
                return NotFound();
            }

            return View(booksGenre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookGenre bookGenre)
        {
            if (!ModelState.IsValid) return View(bookGenre);

            await _bookGenreRepository.Update(bookGenre);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var booksGenre = await _bookGenreRepository.GetById(id);

            if (booksGenre == null)
            {
                return NotFound();
            }

            return View(booksGenre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var booksGenre = await _bookGenreRepository.FirstOrDefault(x => x.Id == id);

            if (booksGenre == null)
            {
                return NotFound();
            }

            await _bookGenreRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
