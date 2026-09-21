namespace KT_31_23_Grigorev_Serafim.DTOs.Students
{

    /// <summary>
    /// Модель создания записи нового студента
    /// </summary>
    public class CreateStudentRequest
    {
        /// <summary>
        /// Имя студента
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия студента
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор группы, в которую зачисляется студент
        /// </summary>
        public int GroupId { get; set; }
    }

}
