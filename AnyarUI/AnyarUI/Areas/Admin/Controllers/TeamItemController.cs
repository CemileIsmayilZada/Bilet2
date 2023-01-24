using Anyar.Business.Utilities;
using Anyar.Business.ViewModels.TeamViewModel;
using Anyar.Core.Entities;
using Anyor.DataAccess.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AnyarUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class TeamItemController : Controller
    {
        private readonly ITeamItemRepository _repository;
        private IValidator<CreateTeamItemVm> _validatorCreate;
        private IValidator<UpdateTeamItemVM> _validatorUpdate;
        private readonly IWebHostEnvironment _env;
        public TeamItemController(ITeamItemRepository repository, IValidator<CreateTeamItemVm> validatorCreate, IWebHostEnvironment env, IValidator<UpdateTeamItemVM> validatorUpdate)
        {
            _repository = repository;
            _validatorCreate = validatorCreate;
            _env = env;
            _validatorUpdate = validatorUpdate;
        }
        public IActionResult Index()
        {
            return View(_repository.GetAll().AsEnumerable());
        }

        public IActionResult Details(int id)
        {
            return View(_repository.GetById(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateTeamItemVm createTeam)
        {
            string fileName = string.Empty;
            ValidationResult result = await _validatorCreate.ValidateAsync(createTeam);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(createTeam);

            if (createTeam is null) return View(createTeam);
            if (createTeam.Image != null)
            {
                if (!createTeam.Image.CheckFileFormat("image/"))
                {
                    ModelState.AddModelError("Image", "Enter Valid format");
                    return View(createTeam);
                }
                if (!createTeam.Image.CheckFileSize(100))
                {
                    ModelState.AddModelError("Image", "Enter Valid Size");
                    return View(createTeam);
                }

                fileName = createTeam.Image.CopyTo(_env.WebRootPath, "assets", "img", "team");



            }
            TeamItem team = new()
            {
                Name = createTeam.Name,
                Position = createTeam.Position,
                Description = createTeam.Description,
                Image = fileName
            };

            _repository.Create(team);
            _repository.SaveChanges();
            return RedirectToAction("Index", "TeamItem");
        }

        public IActionResult Update(int id)
        {
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();

            UpdateTeamItemVM updateTeam = new()
            {
                   Description=team.Description,
                   Name = team.Name,
                   Position=team.Position,
                   ImagePath=team.Image
            };
            return View(updateTeam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(int id, UpdateTeamItemVM updateTeam)
        {

            string fileName = string.Empty;
            ValidationResult result = await _validatorUpdate.ValidateAsync(updateTeam);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(updateTeam);

            if (updateTeam is null) return View(updateTeam);
            if (id != updateTeam.Id) return BadRequest();
             var team=_repository.GetById(id);
            if(team is null) return BadRequest();
            if (updateTeam.Image != null)
            {
                if (!updateTeam.Image.CheckFileFormat("image/"))
                {
                    ModelState.AddModelError("Image", "Enter Valid format");
                    return View(updateTeam);
                }
                if (!updateTeam.Image.CheckFileSize(100))
                {
                    ModelState.AddModelError("Image", "Enter Valid Size");
                    return View(updateTeam);
                }

                fileName = updateTeam.Image.CopyTo(_env.WebRootPath, "assets", "img", "team");

                team.Image = fileName;
            }
            team.Position=updateTeam.Position;
            team.Description=updateTeam.Description;
            team.Name=updateTeam.Name;

            _repository.Update(team);
            _repository.SaveChanges();

            return RedirectToAction("Index", "TeamItem");
        }
        public IActionResult Delete(int id)
        {
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();

            Helper.Delete(_env.WebRootPath, "assets", "img", "team",team.Image);

            _repository.Delete(team);
            _repository.SaveChanges();
            return RedirectToAction("Index", "TeamItem");

        }
    }
}
