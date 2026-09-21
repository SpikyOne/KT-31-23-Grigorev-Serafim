namespace KT_31_23_Grigorev_Serafim.DTOs.Students
{

    /// <summary>
    /// Модель обновления данных студента
    /// </summary>
    public class UpdateStudentRequest
    {
        /// <summary>
        /// Идентификатор обновляемого студента
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Обновленное имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Обновленная фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор новой группы для перевода
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Статус отчисления/удаления (true - отчислен/удален, false - активен)
        /// </summary>
        public bool IsDeleted { get; set; }
    }

}
