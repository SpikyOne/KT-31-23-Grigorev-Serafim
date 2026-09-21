using System.Diagnostics;




namespace KT_31_23_Grigorev_Serafim.Models
{

    /// <summary>
    /// Доменная модель студента
    /// </summary>
    public class Student
    {

        /// <summary>Идентификатор студента</summary>
        public int StudentId { get; set; }

        /// <summary>Имя студента</summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>Фамилия студента</summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>Признак логического удаления/отчисления</summary>
        public bool IsDeleted { get; set; }


        // Внешний ключ и навигация на Group
        /// <summary>Идентификатор учебной группы</summary>
        public int GroupId { get; set; }

        /// <summary>Навигационное свойство: учебная группа</summary>
        public Group Group { get; set; } = null!;


        // Навигационное свойство: у одного студента много оценок
        /// <summary>Коллекция оценок студента</summary>
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    }

}
