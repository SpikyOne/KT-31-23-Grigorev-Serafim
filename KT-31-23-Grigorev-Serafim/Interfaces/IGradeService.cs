using KT_31_23_Grigorev_Serafim.DTOs.Grades;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    /// <summary>
    /// Интерфейс сервиса для управления успеваемостью студентов
    /// </summary>
    public interface IGradeService
    {

        // Получение оценок конкретного студента (и фильтрация по другим параметрам)
        /// <summary>
        /// Получение оценок по фильтру
        /// </summary>
        Task<GradeResponse[]> GetGradesByFilterAsync(GradeFilter filter, CancellationToken cancellationToken = default);


        // Средний балл по предмету в группе
        /// <summary>
        /// Получение среднего балла группы по конкретной дисциплине
        /// </summary>
        Task<double?> GetAverageGradeByGroupAndDisciplineAsync(string groupName, string disciplineName, CancellationToken cancellationToken = default);


        // Средний балл по году (курсу)
        /// <summary>
        /// Получение среднего балла по курсу
        /// </summary>
        Task<double?> GetAverageGradeByCourseAsync(int course, CancellationToken cancellationToken = default);


        /// <summary>
        /// Добавление новой оценки
        /// </summary>
        Task<GradeResponse> AddGradeAsync(CreateGradeRequest request, CancellationToken cancellationToken = default);


        /// <summary>
        /// Обновление существующей оценки
        /// </summary>
        Task<GradeResponse> UpdateGradeAsync(UpdateGradeRequest request, CancellationToken cancellationToken = default);


        /// <summary>
        /// Физическое удаление оценки
        /// </summary>
        Task DeleteGradeAsync(int gradeId, CancellationToken cancellationToken = default);

    }

}
