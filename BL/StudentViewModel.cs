using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace BL
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        [Required] public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid GroupId { get; set; }
        
        public String GroupName { get; set; }
        
        public StudentViewModel() { }
        public StudentViewModel(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            GroupId = student.GroupId;
            GroupName = student.Group.Name;
        }

        public static IQueryable<StudentViewModel> GetStudents(IStudentRepository repository)
        {
            return (repository.AllItems as DbSet<Student>)!
                .Include(item => item.Group)
                .Select(item => new StudentViewModel(item));
        }

        public static StudentViewModel GetStudentById(IStudentRepository repository, Guid id)
        {
            return (repository.AllItems as DbSet<Student>)!
                .Where(item => item.Id == id)
                .Include(item => item.Group)
                .Select(item => new StudentViewModel(item))
                .First();
        }

        public static implicit operator Student(StudentViewModel model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                GroupId = model.GroupId
            };
        }

        public bool IsEmpty()
        {
            return FirstName == null;
        }

        public static bool DeleteStudent(IStudentRepository repository, Guid id)
        {
            return (repository.DeleteItemAsync(id)).IsCompletedSuccessfully;
        }
    }
}