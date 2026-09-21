namespace KT_31_23_Grigorev_Serafim.Models
{

    /// <summary>
    /// Доменная модель учебной группы
    /// </summary>
    public class Group
    {

        /// <summary>Идентификатор группы</summary>
        public int GroupId { get; set; }

        /// <summary>Название группы</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Номер курса</summary>
        public int Course { get; set; }

        /// <summary>Признак логического удаления группы</summary>
        public bool IsDeleted { get; set; }


        // Внешний ключ и навигация на Specialty
        /// <summary>Идентификатор специальности</summary>
        public int SpecialtyId { get; set; }

        /// <summary>Навигационное свойство: специальность</summary>
        public Specialty Specialty { get; set; } = null!;


        // Навигационное свойство: у одной группы много студентов
        /// <summary>Коллекция студентов, входящих в группу</summary>
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }

}
