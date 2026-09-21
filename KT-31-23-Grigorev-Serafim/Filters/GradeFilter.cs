namespace KT_31_23_Grigorev_Serafim.Filters
{

    /// <summary>
    /// Фильтр для поиска оценок в журнале успеваемости
    /// </summary>
    public class GradeFilter
    {
        /// <summary>
        /// Идентификатор конкретного студента
        /// </summary>
        public int? StudentId { get; set; }

        /// <summary>
        /// Точное название группы
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// Точное название учебной дисциплины
        /// </summary>
        public string? DisciplineName { get; set; }

        /// <summary>
        /// Номер курса
        /// </summary>
        public int? Course { get; set; }
    }

}
