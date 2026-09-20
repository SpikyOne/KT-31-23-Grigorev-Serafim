using KT_31_23_Grigorev_Serafim.DTOs.Grades;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{
    public interface IGradeService
    {

        // Получение оценок конкретного студента (и фильтрация по другим параметрам)
        Task<GradeResponse[]> GetGradesByFilterAsync(GradeFilter filter, CancellationToken cancellationToken = default);


        // Средний балл по предмету в группе
        Task<double?> GetAverageGradeByGroupAndDisciplineAsync(string groupName, string disciplineName, CancellationToken cancellationToken = default);


        // Средний балл по году (курсу)
        Task<double?> GetAverageGradeByCourseAsync(int course, CancellationToken cancellationToken = default);

    }

}
