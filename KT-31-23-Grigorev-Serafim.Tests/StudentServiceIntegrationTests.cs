using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Models;
using KT_31_23_Grigorev_Serafim.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;




namespace KT_31_23_Grigorev_Serafim.Tests
{

    /// <summary>
    /// Интеграционные тесты для проверки бизнес-логики <see cref="StudentService"/> с использованием In-Memory БД.
    /// </summary>
    public class StudentServiceIntegrationTests : IntegrationTestBase
    {

        /// <summary>
        /// Проверяет добавление нового студента через сервис:
        /// корректность формируемого DTO-ответа и сохранение записи в базе данных.
        /// </summary>
        [Fact]
        public async Task AddStudentAsync_ValidData_CreatesStudentInDb()
        {
            using var context = CreateContext();

            // Берем группу КТ-31-23, которая уже создана в сидинге
            var group = await context.Groups.FirstAsync(g => g.Name == "КТ-31-23");

            var studentService = new StudentService(context);
            var request = new DTOs.Students.CreateStudentRequest
            {
                FirstName = "Алексей",
                LastName = "Смирнов",
                GroupId = group.GroupId
            };

            var response = await studentService.AddStudentAsync(request, CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal("Алексей", response.FirstName);

            var dbStudent = await context.Students.FirstOrDefaultAsync(s => s.StudentId == response.StudentId);
            Assert.NotNull(dbStudent);
            Assert.Equal("Смирнов", dbStudent.LastName);
        }

    }

}
