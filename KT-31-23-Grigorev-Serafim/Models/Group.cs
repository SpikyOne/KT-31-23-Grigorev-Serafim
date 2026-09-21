using System.Text.RegularExpressions;




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


        /// <summary>
        /// Проверка наименования группы на соответствие шаблону (например: КТ-31-23 или ИВТ-1-23)
        /// </summary>
        public bool IsValidGroupName()
        {
            if (string.IsNullOrWhiteSpace(Name)) return false;

            // Шаблон: 2-4 буквы кириллицы, дефис, 1-2 цифры, дефис, 2 цифры
            var pattern = @"^[А-Яа-я]{2,4}-\d{1,2}-\d{2}$";
            return Regex.IsMatch(Name, pattern);
        }

    }

}
