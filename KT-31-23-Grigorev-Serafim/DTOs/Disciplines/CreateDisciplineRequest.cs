namespace KT_31_23_Grigorev_Serafim.DTOs.Disciplines
{

    /// <summary>
    /// Модель создания новой учебной дисциплины
    /// </summary>
    public class CreateDisciplineRequest
    {
        /// <summary>
        /// Полное название дисциплины (например, "Базы данных")
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }

}
