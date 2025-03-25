using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Courses
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название курса обязательно")]
        [StringLength(100, ErrorMessage = "Название курса не может превышать 100 символов")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Описание курса обязательно")]
        [StringLength(500, ErrorMessage = "Описание курса не может превышать 500 символов")]
        public required string Description { get; set; }

        [Url(ErrorMessage = "Некорректный URL изображения")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Статус курса обязателен")]
        public required string Status { get; set; }

        [Required(ErrorMessage = "Категория курса обязательна")]
        public required string Category { get; set; }

        public int UserProfileId { get; set; } 
        public UserProfile ? UserProfile { get; set; } 
    }
}
