namespace KT_31_23_Grigorev_Serafim.DTOs.Students
{

    public class StudentResponse
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }

}
