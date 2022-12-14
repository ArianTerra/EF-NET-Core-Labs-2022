using System.Xml.Linq;
using System.Xml.Serialization;
using Task3.Console;

var context = new DatabaseContext();

context.XmlData.RemoveRange(context.XmlData);
context.SaveChanges();

var users = new List<User>()
{
    new (0, "Arthur", "Smith", new DateTime(1999, 6, 3)),
    new (1, "Diana", "Theodoro", new DateTime(2000, 2, 13)),
    new (2, "John", "Star", new DateTime(2001, 4, 23)),
    new (3, "Bob", "Xantos", new DateTime(2002, 1, 2)),
    new (4, "Jeanne", "Terina", new DateTime(2000, 12, 8))
};

var serializer = new XmlSerializer(typeof(List<User>));

using var stringWriter = new StringWriter();
serializer.Serialize(stringWriter, users);

var xmlData = new XmlData()
{
    XmlString = stringWriter.ToString(),
    XElement = XElement.Parse(stringWriter.ToString())
};

context.XmlData.Add(xmlData);
context.SaveChanges();

foreach (var data in context.XmlData)
{
    Console.WriteLine($"ID: {data.Id}\nXElement: {data.XElement}\nXmlString:{data.XmlString}");
}