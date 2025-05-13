using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ввод номера обязателен")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Ввод биографии обязателен")]
        [StringLength(200)]
        public required string Bio { get; set; }

        [Required(ErrorMessage = "Ввод даты обязателен")]
        public DateTime DateOfBirth { get; set; }

        public List<Courses> Courses { get; set; } = new List<Courses>(); // Уже инициализировано
    }
}