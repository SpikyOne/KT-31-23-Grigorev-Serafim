namespace KT_31_23_Grigorev_Serafim.DTOs.Grades
{

    public class CreateGradeRequest
    {
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
    }

}
