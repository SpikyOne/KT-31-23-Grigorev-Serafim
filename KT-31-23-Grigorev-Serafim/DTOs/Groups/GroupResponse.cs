namespace KT_31_23_Grigorev_Serafim.DTOs.Groups
{

    /// <summary>
    /// Модель ответа с информацией об учебной группе
    /// </summary>
    public class GroupResponse
    {

        /// <summary>Идентификатор группы</summary>
        public int GroupId { get; set; }

        /// <summary>Название группы</summary>
        public string Name { get; set; }

        /// <summary>Номер курса</summary>
        public int Course { get; set; }

        /// <summary>Название специальности</summary>
        public string SpecialtyName { get; set; }

    }

}