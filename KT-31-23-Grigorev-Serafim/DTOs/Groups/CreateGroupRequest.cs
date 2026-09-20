namespace KT_31_23_Grigorev_Serafim.DTOs.Groups
{

    public class CreateGroupRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Course { get; set; }
        public int SpecialtyId { get; set; }
    }

}
