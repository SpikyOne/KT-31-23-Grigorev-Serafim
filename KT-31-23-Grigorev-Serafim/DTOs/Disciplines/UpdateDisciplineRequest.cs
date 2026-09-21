namespace KT_31_23_Grigorev_Serafim.DTOs.Disciplines
{

    /// <summary>
    /// Модель обновления данных учебной дисциплины
    /// </summary>
    public class UpdateDisciplineRequest
    {
        /// <summary>
        /// Уникальный идентификатор обновляемой дисциплины
        /// </summary>
        public int DisciplineId { get; set; }

        /// <summary>
        /// Новое название дисциплины
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Статус логического удаления (true - удалена, false - активна)
        /// </summary>
        public bool IsDeleted { get; set; }
    }

}
