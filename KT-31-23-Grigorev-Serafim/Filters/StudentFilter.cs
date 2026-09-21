namespace KT_31_23_Grigorev_Serafim.Filters
{

    /// <summary>
    /// Фильтр для поиска студентов
    /// </summary>
    public class StudentFilter
    {
        /// <summary>
        /// Точное название группы (например, "ИВТ-1-23")
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// Частичное или точное совпадение имени или фамилии (в любом порядке)
        /// </summary>
        public string? FIO { get; set; }

        /// <summary>
        /// Флаг состояния записи студента (true - удален, false - активен, null - все)
        /// </summary>
        public bool? IsDeleted { get; set; }
    }

}
