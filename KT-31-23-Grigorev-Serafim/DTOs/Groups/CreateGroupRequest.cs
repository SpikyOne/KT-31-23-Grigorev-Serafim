namespace KT_31_23_Grigorev_Serafim.DTOs.Groups
{

    /// <summary>
    /// Модель создания новой группы
    /// </summary>
    public class CreateGroupRequest
    {
        /// <summary>
        /// Уникальное название группы (например, "ИВТ-1-23")
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Курс обучения (от 1 до 5)
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Идентификатор существующей специальности в БД
        /// </summary>
        public int SpecialtyId { get; set; }
    }

}
