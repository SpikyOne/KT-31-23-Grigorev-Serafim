namespace KT_31_23_Grigorev_Serafim.Filters
{

    /// <summary>
    /// Фильтр для поиска учебных дисциплин
    /// </summary>
    public class DisciplineFilter
    {
        /// <summary>
        /// Частичное или точное совпадение названия дисциплины
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Флаг состояния (true - удалена, false - активна, null - все)
        /// </summary>
        public bool? IsDeleted { get; set; }
    }

}
