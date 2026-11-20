using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KT_31_23_Grigorev_Serafim.Models
{
    /// <summary>
    /// Доменная модель учебной дисциплины
    /// </summary>
    public class Discipline
    {
        /// <summary>Идентификатор дисциплины</summary>
        public int DisciplineId { get; set; }

        /// <summary>Наименование дисциплины</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Признак логического удаления записи</summary>
        public bool IsDeleted { get; set; }

        // Навигационное свойство: у дисциплины много оценок
        /// <summary>Коллекция оценок по данной дисциплине</summary>
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

        /// <summary>
        /// Метод для юнит-тестирования: проверка корректности названия дисциплины по 3 критериям
        /// </summary>
        public static bool IsValidDisciplineName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (name.Length > 150)
                return false;

            var pattern = @"^[А-Яа-я][А-Яа-я -]*(?: {1,2}[А-Яа-я -]+)*$";
            // var pattern = @"^(?=.*[А-Яа-я])(?: {1,2}|-|[А-Яа-я])+$";

            // Или если строку можно начинать только с буквы
            // var pattern = @"^[А-Яа-я][А-Яа-я -]*(?: {1,2}[А-Яа-я -]+)*$";

            // Старая версия
            // var pattern = @"^[А-Яа-я]+(?:[\s\-][А-Яа-я]+)*$";

            return Regex.IsMatch(name, pattern);

        }
    }
}
