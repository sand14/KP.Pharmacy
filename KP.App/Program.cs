using KP.Core.DomainModels;
using KP.Services.Product;
using KP.Services.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NC.SchoolManagement.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceProvider serviceProvider = null;
            ServiceProvider serviceProvider = SetUpServices();

            //GetStudentsList(serviceProvider);
            //CreateStudent(serviceProvider);
            //GetStudentsList(serviceProvider);
        }


        private static ServiceProvider SetUpServices()
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=Pharmacy;Trusted_Connection=True;";
            var services = new ServiceCollection();
            services.AddSingleton<IProductService, ProductService>();
            services.AddDbContext<PharmacyContext>(options => options.UseSqlServer(connectionString));
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        private static IProductService GetStudentService(ServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new Exception("Service provider not initialized!");

            return serviceProvider.GetService<IStudentService>() ?? throw new Exception("Service not registered!");

        }


        #region Students methods

        private static void GetStudentsList(ServiceProvider serviceProvider)
        {
            try
            {
                var studentService = GetStudentService(serviceProvider);
                var students = studentService.GetStudents();

                if (students.Any())
                {
                    foreach (var student in students)
                        Console.WriteLine($"{student.StudentId}, {student.FirstName} {student.LastName}");
                }
                else
                {
                    Console.WriteLine("No students found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

