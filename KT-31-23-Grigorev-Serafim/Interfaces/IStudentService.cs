using KT_31_23_Grigorev_Serafim.DTOs.Students;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    /// <summary>
    /// Интерфейс сервиса для управления студентами
    /// </summary>
    public interface IStudentService
    {

        /// <summary>
        /// Получение списка студентов по фильтру
        /// </summary>
        Task<StudentResponse[]> GetStudentsByFilterAsync(StudentFilter filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// Добавление нового студента
        /// </summary>
        Task<StudentResponse> AddStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновление данных студента
        /// </summary>
        Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request, CancellationToken cancellationToken = default);


        /// <summary>
        /// Логическое удаление студента
        /// </summary>
        Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);

    }

}
