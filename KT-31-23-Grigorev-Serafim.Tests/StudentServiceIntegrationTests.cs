using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Models;
using KT_31_23_Grigorev_Serafim.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;




namespace KT_31_23_Grigorev_Serafim.Tests
{

    public class StudentServiceIntegrationTests
    {

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;


        public StudentServiceIntegrationTests()
        {
            // Создаем уникальную базу данных в памяти для каждого запуска
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }


        [Fact]
        public async Task GetStudentsByFilterAsync_FilterByGroupKT3123_ReturnsTwoStudents()
        {
            // Arrange
            using var context = new AppDbContext(_dbContextOptions);

            var specialty = new Specialty { Title = "Информатика", Code = "09.03.01" };

            var group1 = new Group { Name = "КТ-31-23", Course = 3, Specialty = specialty };
            var group2 = new Group { Name = "КТ-41-23", Course = 4, Specialty = specialty };

            await context.Groups.AddRangeAsync(group1, group2);
            await context.SaveChangesAsync();

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Серафим",
                    LastName = "Григорьев",
                    GroupId = group1.GroupId
                },
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    GroupId = group1.GroupId
                },
                new Student
                {
                    FirstName = "Пётр",
                    LastName = "Петров",
                    GroupId = group2.GroupId
                }
            };

            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();

            var studentService = new StudentService(context);

            var filter = new StudentFilter
            {
                GroupName = "КТ-31-23"
            };

            // Act
            var studentsResult = await studentService.GetStudentsByFilterAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult.Length);
            Assert.All(studentsResult, s => Assert.Equal("КТ-31-23", s.GroupName));
        }


        [Fact]
        public async Task AddStudentAsync_ValidData_CreatesStudentInDb()
        {
            // Arrange
            using var context = new AppDbContext(_dbContextOptions);

            var group = new Group
            {
                Name = "КТ-31-23",
                Course = 3,
                Specialty = new Specialty { Title = "ПО", Code = "09.03.04" }
            };
            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();

            var studentService = new StudentService(context);
            var request = new DTOs.Students.CreateStudentRequest
            {
                FirstName = "Алексей",
                LastName = "Смирнов",
                GroupId = group.GroupId
            };

            // Act
            var response = await studentService.AddStudentAsync(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Алексей", response.FirstName);

            var dbStudent = await context.Students.FirstOrDefaultAsync(s => s.StudentId == response.StudentId);
            Assert.NotNull(dbStudent);
            Assert.Equal("Смирнов", dbStudent.LastName);
        }

    }

}
