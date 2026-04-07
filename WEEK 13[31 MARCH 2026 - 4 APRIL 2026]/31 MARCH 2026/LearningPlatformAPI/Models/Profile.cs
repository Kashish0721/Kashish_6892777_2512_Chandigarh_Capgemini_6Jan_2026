namespace LearningPlatformAPI.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}