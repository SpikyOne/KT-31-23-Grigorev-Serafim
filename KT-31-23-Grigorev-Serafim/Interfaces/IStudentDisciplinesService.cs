using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    /// <summary>
    /// Интерфейс сервиса получения дисциплин студента
    /// </summary>
    public interface IStudentDisciplinesService
    {

        /// <summary>
        /// Получение уникального списка наименований дисциплин, по которым у студента оценка 5
        /// </summary>
        /// <param name="filter">Модель фильтра с фамилией студента</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Массив названий дисциплин без повторов</returns>
        Task<string[]> GetExcellentDisciplinesByLastNameAsync(StudentLastNameFilter filter, CancellationToken cancellationToken = default);

    }

}