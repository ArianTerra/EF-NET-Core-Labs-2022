using Microsoft.EntityFrameworkCore;
using Task1.BL.Services;
using Task1.DAL;
using Task1.DAL.Repositories;

namespace Task1.WEB;

public static class DependencyInjectionExtension
{
    public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(BL.MappingProfile));

        services.AddDbContext<DatabaseContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("Default")
            ));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
    }
}