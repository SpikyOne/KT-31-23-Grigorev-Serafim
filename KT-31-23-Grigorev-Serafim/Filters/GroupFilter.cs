namespace KT_31_23_Grigorev_Serafim.Filters
{

    /// <summary>
    /// Фильтр для поиска групп
    /// </summary>
    public class GroupFilter
    {
        /// <summary>
        /// Частичное или точное совпадение названия специальности
        /// </summary>
        public string? SpecialtyName { get; set; }

        /// <summary>
        /// Номер курса (например, от 1 до 5)
        /// </summary>
        public int? Course { get; set; }

        /// <summary>
        /// Флаг состояния группы (true - удалена, false - активна, null - все)
        /// </summary>
        public bool? IsDeleted { get; set; }
    }

}
