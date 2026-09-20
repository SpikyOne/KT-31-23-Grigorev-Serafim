namespace KT_31_23_Grigorev_Serafim.DTOs.Students
{

    public class CreateStudentRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int GroupId { get; set; }
    }

}
