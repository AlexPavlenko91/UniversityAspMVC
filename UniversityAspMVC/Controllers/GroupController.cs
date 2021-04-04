using System;
using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace WebApp.Controllers
{
    public class GroupController : Controller
    {
        IGroupRepository _repository;
        // GET
        public IActionResult Index()
        {
            return View(GroupViewModel.GetGroups(_repository));
        }
        
        public GroupController(IGroupRepository repository)
        {
            _repository = repository;
        }
        

        public IActionResult Create(GroupViewModel model)
        {
            if (model.IsEmpty())
                return View(model);

            _repository.AddItemAsync(model);
            return Redirect("Index");
        }

        public IActionResult Edit(Guid? Id)
        {
            if (Id.HasValue)
            {
                
                return View(GroupViewModel.GetGroupById(_repository, Id.Value));
            } return NotFound(); 
        }

        public IActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                return View(GroupViewModel.DeleteGroup(_repository, (Guid)id));
            } return NotFound(); 
        }
    }
}