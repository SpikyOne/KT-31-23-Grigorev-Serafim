namespace KT_31_23_Grigorev_Serafim.Models
{

    /// <summary>
    /// Доменная модель оценки успеваемости
    /// </summary>
    public class Grade
    {

        /// <summary>Идентификатор оценки</summary>
        public int GradeId { get; set; }

        /// <summary>Значение оценки</summary>
        public int Value { get; set; }


        // Внешний ключ и навигация на Student
        /// <summary>Идентификатор студента</summary>
        public int StudentId { get; set; }

        /// <summary>Навигационное свойство: студент</summary>
        public Student Student { get; set; } = null!;


        // Внешний ключ и навигация на Discipline
        /// <summary>Идентификатор дисциплины</summary>
        public int DisciplineId { get; set; }

        /// <summary>Навигационное свойство: дисциплина</summary>
        public Discipline Discipline { get; set; } = null!;

    }

}
