using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAll();
            return View(authors);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid) return View(author);

            await _authorRepository.Add(author);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!ModelState.IsValid) return View(author);

            await _authorRepository.Update(author);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var author = await _authorRepository.FirstOrDefault(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            await _authorRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
