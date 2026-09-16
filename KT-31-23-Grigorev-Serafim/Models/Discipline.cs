using System.Diagnostics;

namespace KT_31_23_Grigorev_Serafim.Models
{

    public class Discipline
    {

        public int DisciplineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        // Навигационное свойство: у дисциплины много оценок
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    }

}
