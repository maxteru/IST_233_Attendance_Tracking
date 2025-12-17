using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingMVC.Models
{
    [Table("Students")]
    public class Student
    {
        /// <summary>
        /// id студента
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Студент
        /// </summary>
        [MaxLength(100)]
        public string StudentName { get; set; } = string.Empty;
        /// <summary>
        /// Фото студента
        /// </summary>
        [MaxLength(100)]
        public string ImageFileName { get; set; } = string.Empty;
        /// <summary>
        /// Количество прогулов и пропусков по уважительным и неуважительным  причинам
        /// </summary>
        [MaxLength(50)]
        public string NumberPasses { get; set; } = string.Empty;
        /// <summary>
        /// Количество посещенных занятий
        /// </summary>
        [MaxLength(50)]
        public string NumberVisits { get; set; } = string.Empty;
        /// <summary>
        /// Причина пропуска занятий
        /// </summary>
        [MaxLength(255)]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Статус причины
        /// </summary>
        [MaxLength(255)]
        public string StatusReason { get; set; } = string.Empty; 
        /// <summary>
        /// Группа
        /// </summary>
        [MaxLength(100)] 
        public string Group { get; set; } = string.Empty; 
        /// <summary>
        /// Преподаватель
        /// </summary>
        [MaxLength(100)] 
        public string Teacher { get; set; } = string.Empty;
        /// <summary>
        /// Дата внесения сведений
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
