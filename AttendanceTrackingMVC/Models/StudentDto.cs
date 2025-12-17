using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingMVC.Models
{
    public class StudentDto
    {
        /// <summary>
        /// id студента
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Студент
        /// </summary>
        [Required, MaxLength(100)]
        public string StudentName { get; set; } = string.Empty;
        /// <summary>
        /// Фото студента
        /// </summary>        
        public IFormFile? ImageFile { get; set; }
        /// <summary>
        /// Количество прогулов и пропусков по уважительным и неуважительным причинам
        /// </summary>
        [Required, MaxLength(50)]
        public string NumberPasses { get; set; } = string.Empty;
        /// <summary>
        /// Количество посещенных занятий
        /// </summary>
        [Required, MaxLength(50)]
        public string NumberVisits { get; set; } = string.Empty;
        /// <summary>
        /// Причина пропуска занятий
        /// </summary>
        [Required, MaxLength(255)]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Статус причины
        /// </summary>
        [Required, MaxLength(255)]
        public string StatusReason { get; set; } = string.Empty;
        /// <summary>
        /// Группа
        /// </summary>
        [Required, MaxLength(100)]
        public string Group { get; set; } = string.Empty;
        /// <summary>
        /// Преподаватель
        /// </summary>
        [Required, MaxLength(100)]
        public string Teacher { get; set; } = string.Empty;
        /// <summary>
        /// Дата внесения сведений
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
