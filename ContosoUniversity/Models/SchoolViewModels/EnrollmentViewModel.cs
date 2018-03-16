namespace ContosoUniversity.Models.SchoolViewModels
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }
        public Grade? Grade { get; set; }
        public string CourseTitle { get; set; }
        public string StudentName { get; set; }
    }
}