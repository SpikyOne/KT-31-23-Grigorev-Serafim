namespace KT_31_23_Grigorev_Serafim.DTOs.Grades
{

    /// <summary>
    /// Модель ответа с информацией об оценке студента
    /// </summary>
    public class GradeResponse
    {

        /// <summary>Идентификатор оценки</summary>
        public int GradeId { get; set; }

        /// <summary>Значение оценки</summary>
        public int Value { get; set; }

        /// <summary>Фамилия и имя студента</summary>
        public string StudentName { get; set; } = string.Empty;

        /// <summary>Название учебной дисциплины</summary>
        public string DisciplineName { get; set; } = string.Empty;

        /// <summary>Название группы студента</summary>
        public string GroupName { get; set; } = string.Empty;

        /// <summary>Номер курса</summary>
        public int Course { get; set; }

    }

}
