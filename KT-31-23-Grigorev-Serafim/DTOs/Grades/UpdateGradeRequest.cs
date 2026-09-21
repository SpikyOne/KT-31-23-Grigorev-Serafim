namespace KT_31_23_Grigorev_Serafim.DTOs.Grades
{

    /// <summary>
    /// Модель для изменения существующей оценки
    /// </summary>
    public class UpdateGradeRequest
    {
        /// <summary>
        /// Идентификатор обновляемой оценки
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// Новое значение оценки
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Идентификатор дисциплины
        /// </summary>
        public int DisciplineId { get; set; }
    }

}
