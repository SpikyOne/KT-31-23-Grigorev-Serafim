using System.Diagnostics;

namespace KT_31_23_Grigorev_Serafim.Models
{

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        // Внешний ключ и навигация на Group
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        // Навигационное свойство: у одного студента много оценок
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }

}
