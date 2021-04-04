using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace BL
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        
        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public List<Student> Students { get; set; }
        
        
        public GroupViewModel() { }
        public GroupViewModel(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            FacultyId = group.FacultyId;
            Faculty = group.Faculty;
            Students = group.Students;
        }

        public static IQueryable<GroupViewModel> GetGroups(IGroupRepository repository)
        {
            return (repository.AllItems as DbSet<Group>)
                .Include(item => item.Faculty)
                .Include(item => item.Students)
                .Select(item => new GroupViewModel(item));
        }

        public static GroupViewModel GetGroupById(IGroupRepository repository, Guid id)
        {
            return (repository.AllItems as DbSet<Group>)
                .Where(item => item.Id == id)
                .Include(item => item.Faculty)
                .Include(item => item.Students)
                .Select(item => new GroupViewModel(item))
                .First();
        }

        public static implicit operator Group(GroupViewModel model)
        {
            return new Group()
            {
                Id = model.Id,
                Name = model.Name,
                FacultyId = model.FacultyId,
                Faculty = model.Faculty,
                Students = model.Students
            };
        }

        public bool IsEmpty()
        {
            return Name == null;
        }

        public static bool DeleteGroup(IGroupRepository repository, Guid id)
        {
            return (repository.DeleteItemAsync(id)).IsCompletedSuccessfully;
        }
    }
}