namespace KT_31_23_Grigorev_Serafim.DTOs.Grades
{

    public class GradeResponse
    {
        public int GradeId { get; set; }
        public int Value { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string DisciplineName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public int Course { get; set; }
    }

}
