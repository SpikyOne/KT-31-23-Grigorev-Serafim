namespace KT_31_23_Grigorev_Serafim.DTOs.Students
{

    /// <summary>
    /// Модель ответа с информацией о студенте
    /// </summary>
    public class StudentResponse
    {

        /// <summary>Идентификатор студента</summary>
        public int StudentId { get; set; }

        /// <summary>Имя студента</summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>Фамилия студента</summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>Название учебной группы</summary>
        public string GroupName { get; set; } = string.Empty;

        /// <summary>Статус логического удаления/отчисления</summary>
        public bool IsDeleted { get; set; }

    }

}
