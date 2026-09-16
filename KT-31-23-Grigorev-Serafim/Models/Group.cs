namespace KT_31_23_Grigorev_Serafim.Models
{

    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Course { get; set; }
        public bool IsDeleted { get; set; }

        // Внешний ключ и навигация на Specialty
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;

        // Навигационное свойство: у одной группы много студентов
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }

}
