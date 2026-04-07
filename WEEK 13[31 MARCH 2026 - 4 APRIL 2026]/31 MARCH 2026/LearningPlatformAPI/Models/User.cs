namespace LearningPlatformAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Profile Profile { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}