using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(14)]
        public string NationalId { get; set; } = null!;

        [StringLength(11)]
        public string? PhoneNumber { get; set; } 
    }
}
