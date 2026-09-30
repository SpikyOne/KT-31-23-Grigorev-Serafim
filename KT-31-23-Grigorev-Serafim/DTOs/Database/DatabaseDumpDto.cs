namespace KT_31_23_Grigorev_Serafim.DTOs.Database
{

    /// <summary>
    /// Объект передачи данных (DTO), содержащий полный снимок (дамп) всех таблиц базы данных.
    /// </summary>
    public class DatabaseDumpDto
    {

        /// <summary>
        /// Список всех зарегистрированных специальностей.
        /// </summary>
        public List<SpecialtyDumpDto> Specialties { get; set; } = new();

        /// <summary>
        /// Список всех учебных групп.
        /// </summary>
        public List<GroupDumpDto> Groups { get; set; } = new();

        /// <summary>
        /// Список всех студентов.
        /// </summary>
        public List<StudentDumpDto> Students { get; set; } = new();

        /// <summary>
        /// Список всех учебных дисциплин.
        /// </summary>
        public List<DisciplineDumpDto> Disciplines { get; set; } = new();

        /// <summary>
        /// Список всех выставленных оценок.
        /// </summary>
        public List<GradeDumpDto> Grades { get; set; } = new();

    }


    /// <summary>
    /// DTO специальности для экспорта и импорта в составе дампа базы данных.
    /// </summary>
    public class SpecialtyDumpDto
    {

        /// <summary>
        /// Первичный ключ (уникальный идентификатор) специальности.
        /// </summary>
        public int SpecialtyId { get; set; }

        /// <summary>
        /// Наименование специальности.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Код специальности по классификатору (например, "09.03.04").
        /// </summary>
        public string Code { get; set; } = string.Empty;

    }


    /// <summary>
    /// DTO учебной группы для экспорта и импорта в составе дампа базы данных.
    /// </summary>
    public class GroupDumpDto
    {

        /// <summary>
        /// Первичный ключ (уникальный идентификатор) группы.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Шифр или наименование группы (например, "КТ-31-23").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Номер текущего курса обучения.
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Признак мягкого удаления группы (мягкое удаление).
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Внешний ключ: идентификатор связанной специальности.
        /// </summary>
        public int SpecialtyId { get; set; }

    }


    /// <summary>
    /// DTO студента для экспорта и импорта в составе дампа базы данных.
    /// </summary>
    public class StudentDumpDto
    {

        /// <summary>
        /// Первичный ключ (уникальный идентификатор) студента.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Имя студента.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия студента.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Признак мягкого удаления записи студента.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Внешний ключ: идентификатор учебной группы, к которой прикреплен студент.
        /// </summary>
        public int GroupId { get; set; }

    }


    /// <summary>
    /// DTO учебной дисциплины для экспорта и импорта в составе дампа базы данных.
    /// </summary>
    public class DisciplineDumpDto
    {

        /// <summary>
        /// Первичный ключ (уникальный идентификатор) дисциплины.
        /// </summary>
        public int DisciplineId { get; set; }

        /// <summary>
        /// Полное наименование предмета / дисциплины.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Признак мягкого удаления дисциплины.
        /// </summary>
        public bool IsDeleted { get; set; }

    }


    /// <summary>
    /// DTO оценки для экспорта и импорта в составе дампа базы данных.
    /// </summary>
    public class GradeDumpDto
    {

        /// <summary>
        /// Первичный ключ (уникальный идентификатор) записи об оценке.
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// Численное значение оценки (например, от 2 до 5 или балл по шкале).
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Внешний ключ: идентификатор студента, получившего оценку.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Внешний ключ: идентификатор дисциплины, по которой выставлена оценка.
        /// </summary>
        public int DisciplineId { get; set; }

    }

}
