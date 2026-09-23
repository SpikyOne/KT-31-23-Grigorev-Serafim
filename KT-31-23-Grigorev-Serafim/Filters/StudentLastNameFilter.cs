namespace KT_31_23_Grigorev_Serafim.Filters
{

    /// <summary>
    /// Фильтр для поиска отличных дисциплин по фамилии студента
    /// </summary>
    public class StudentLastNameFilter
    {

        /// <summary>
        /// Фамилия студента (например, "Иванов")
        /// </summary>
        public string LastName { get; set; } = string.Empty;

    }

}