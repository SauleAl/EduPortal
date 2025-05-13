using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int CoursesId { get; set; }
        public Courses? Courses { get; set; } // Сделали nullable
        public string UserId { get; set; }
        [Required(ErrorMessage = "Дата бронирования обязательна")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}