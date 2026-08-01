using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers;
using Ticketora.WebUI.Constants;

namespace Ticketora.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
        private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;
        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
        private readonly GetByIdCategoryQueryHandler _getByIdCategoryQueryHandler;

        public CategoryController(CreateCategoryCommandHandler createCategoryCommandHandler, GetCategoryQueryHandler getCategoryQueryHandler, RemoveCategoryCommandHandler removeCategoryCommandHandler, UpdateCategoryCommandHandler updateCategoryCommandHandler, GetByIdCategoryQueryHandler getByIdCategoryQueryHandler)
        {
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _getCategoryQueryHandler = getCategoryQueryHandler;
            _removeCategoryCommandHandler = removeCategoryCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _getByIdCategoryQueryHandler = getByIdCategoryQueryHandler;
        }

        public async Task<IActionResult> CategoryList()
        {
            var categories = await _getCategoryQueryHandler.Handle();
            return View(categories);
        }

        [Authorize(Roles = AppRoles.Admin)]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            await _createCategoryCommandHandler.Handle(command);
            return RedirectToAction("Categories", "Admin");
        }

        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _removeCategoryCommandHandler.Handle(new RemoveCategoryCommand { CategoryId = id });
            return RedirectToAction("Categories", "Admin");
        }
    }
}
