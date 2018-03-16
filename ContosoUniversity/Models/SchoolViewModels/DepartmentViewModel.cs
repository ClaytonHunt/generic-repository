using System;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public InstructorViewModel Administrator { get; set; }
    }
}