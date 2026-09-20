using KT_31_23_Grigorev_Serafim.DTOs.Students;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    public interface IStudentService
    {
        
        Task<StudentResponse[]> GetStudentsByFilterAsync(StudentFilter filter, CancellationToken cancellationToken = default);
        
        Task<StudentResponse> AddStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);
        
        Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request, CancellationToken cancellationToken = default);
        
        Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);

    }

}
