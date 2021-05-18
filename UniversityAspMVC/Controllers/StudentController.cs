using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;

namespace WebApp.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository _repository;
        // GET
        public IActionResult GetAll()
        {
            return View(StudentViewModel.GetStudents(_repository));
        }
        
        public IEnumerable<StudentViewModel> GetAllApi()
        {
            return (StudentViewModel.GetStudents(_repository)).ToList();
        }
        
        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }
        

        public IActionResult Create(StudentViewModel model)
        {
            if (model.IsEmpty())
                return View(model);

            _repository.AddItemAsync(model);
            return Redirect("GetAll");
        }

        public IActionResult Edit(StudentViewModel model)
        {
            if (model!=null)
            {
                if (_repository.ChangeItemAsync(model) != null)
                {
                    return Redirect("GetAll");
                }
            } return NotFound(); 
        }

        public IActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                return View(StudentViewModel.DeleteStudent(_repository, (Guid)id));
            } return NotFound(); 
        }

      
    }
}