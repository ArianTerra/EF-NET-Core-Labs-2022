using AutoMapper;
using Task1.BL.DTO;
using Task1.DAL.DomainModels.TPH;
using Task1.DAL.DomainModels.TPT;

namespace Task1.BL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Article, ArticleDto>();
        CreateMap<ArticleDto, Article>();

        CreateMap<Book, BookDto>();
        CreateMap<BookDto, Book>();

        CreateMap<Student, StudentDto>();
        CreateMap<StudentDto, Student>();

        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherDto, Teacher>();
    }
}