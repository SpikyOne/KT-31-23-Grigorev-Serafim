using System.Text.RegularExpressions;

namespace KT_31_23_Grigorev_Serafim.Models
{

    public class Specialty
    {

        public int SpecialtyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        // Навигационное свойство: у одной специальности много групп
        public ICollection<Group> Groups { get; set; } = new List<Group>();

    }

}
