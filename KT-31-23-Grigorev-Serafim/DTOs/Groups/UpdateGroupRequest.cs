namespace KT_31_23_Grigorev_Serafim.DTOs.Groups
{

    /// <summary>
    /// Модель обновления данных группы
    /// </summary>
    public class UpdateGroupRequest
    {
        /// <summary>
        /// Уникальный идентификатор группы, которую нужно обновить
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Новое название группы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Новый курс обучения
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Обновленный идентификатор специальности
        /// </summary>
        public int SpecialtyId { get; set; }

        /// <summary>
        /// Статус логического удаления (true - отправить в архив, false - восстановить/оставить активной)
        /// </summary>
        public bool IsDeleted { get; set; }
    }

}
