using System.ComponentModel.DataAnnotations;

namespace LibraryPractice.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Required]
        public string MemberName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public ICollection<BorrowRecord>? BorrowRecords { get; set; }
    }
}