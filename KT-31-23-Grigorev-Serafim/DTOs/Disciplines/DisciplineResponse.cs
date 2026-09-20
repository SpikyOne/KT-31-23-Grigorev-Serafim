namespace KT_31_23_Grigorev_Serafim.DTOs.Disciplines
{

    public class DisciplineResponse
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }

}
