namespace KT_31_23_Grigorev_Serafim.Models
{

    public class Grade
    {

        public int GradeId { get; set; }
        public int Value { get; set; }

        // Внешний ключ и навигация на Student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        // Внешний ключ и навигация на Discipline
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; } = null!;

    }

}
