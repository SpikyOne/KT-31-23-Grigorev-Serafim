using System.Text.RegularExpressions;




namespace KT_31_23_Grigorev_Serafim.Models
{

    /// <summary>
    /// Доменная модель специальности
    /// </summary>
    public class Specialty
    {

        /// <summary>Идентификатор специальности</summary>
        public int SpecialtyId { get; set; }

        /// <summary>Полное название специальности</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>Код специальности по классификатору (например, "09.03.01")</summary>
        public string Code { get; set; } = string.Empty;


        // Навигационное свойство: у одной специальности много групп
        /// <summary>Коллекция групп данной специальности</summary>
        public ICollection<Group> Groups { get; set; } = new List<Group>();

    }

}
