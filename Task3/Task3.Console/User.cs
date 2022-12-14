namespace Task3.Console;

public class User
{
    public User()
    {

    }

    public User(int id, string name, string surname, DateTime birthday)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Birthday = birthday;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public DateTime Birthday { get; set; }
}