namespace KT_31_23_Grigorev_Serafim.DTOs.Grades
{

    /// <summary>
    /// Модель выставления новой оценки
    /// </summary>
    public class CreateGradeRequest
    {
        /// <summary>
        /// Значение оценки (например, 2, 3, 4, 5)
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Идентификатор студента, которому выставляется оценка
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Идентификатор дисциплины
        /// </summary>
        public int DisciplineId { get; set; }
    }

}
