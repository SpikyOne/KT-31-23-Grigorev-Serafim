namespace KT_31_23_Grigorev_Serafim.DTOs.Disciplines
{

    /// <summary>
    /// Модель ответа с данными учебной дисциплины
    /// </summary>
    public class DisciplineResponse
    {

        /// <summary>Идентификатор дисциплины</summary>
        public int DisciplineId { get; set; }

        /// <summary>Название дисциплины</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Статус логического удаления</summary>
        public bool IsDeleted { get; set; }

    }

}
