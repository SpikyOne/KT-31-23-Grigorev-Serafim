using System.Diagnostics;




namespace KT_31_23_Grigorev_Serafim.Models
{

    /// <summary>
    /// Доменная модель учебной дисциплины
    /// </summary>
    public class Discipline
    {

        /// <summary>Идентификатор дисциплины</summary>
        public int DisciplineId { get; set; }

        /// <summary>Наименование дисциплины</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Признак логического удаления записи</summary>
        public bool IsDeleted { get; set; }

        // Навигационное свойство: у дисциплины много оценок
        /// <summary>Коллекция оценок по данной дисциплине</summary>
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    }

}
