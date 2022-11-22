using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Task2.Console;

var context = new DatabaseContext();
context.Vassals.RemoveRange(context.Vassals);
context.SaveChanges();

//Populate table with data
MedievalVassal[] data =
{
    new(HierarchyId.Parse("/"), "King Richard"),
    new(HierarchyId.Parse("/0/"), "Duke of York"),
    new(HierarchyId.Parse("/1/"), "Duke of Liverpool"),
    new(HierarchyId.Parse("/2/"), "Duke of Sheffield"),
    new(HierarchyId.Parse("/0/0/"), "Knight Orson"),
    new(HierarchyId.Parse("/0/1/"), "Knight Wilhelm"),
    new(HierarchyId.Parse("/1/0/"), "Knight Bob"),
    new(HierarchyId.Parse("/1/0/0/"), "Peasant1"),
    new(HierarchyId.Parse("/1/0/1/"), "Peasant2"),
};

context.Vassals.AddRange(data);
context.SaveChanges();

// show all
DisplayData(context.Vassals, "All vassals");

// display all descendants of 'Duke of Liverpool'
var duke = context.Vassals.FirstOrDefault(x => x.Name == "Duke of Liverpool");
var descendants = context.Vassals.Where(x => x.MedievalVassalId.IsDescendantOf(duke.MedievalVassalId));

DisplayData(descendants, "Child elements");

// display all parents of 'Peasant1'
var peasant = context.Vassals.Where(x => x.Name == "Peasant1").FirstOrDefault();

IEnumerable<MedievalVassal> GetAllAncestors(MedievalVassal node, IEnumerable<MedievalVassal> hierarchy)
{
    while (node.MedievalVassalId.GetLevel() > 0)
    {
        var ancestor = hierarchy.FirstOrDefault(x => x.MedievalVassalId == node.MedievalVassalId.GetAncestor(1));
        yield return ancestor;
        node = ancestor;
    }
}

DisplayData(GetAllAncestors(peasant, context.Vassals), "Ancestors");

// delete 'Knight Bob' tree branch
var bob = context.Vassals.FirstOrDefault(x => x.Name == "Knight Bob");
var branch = context.Vassals.Where(x => x.MedievalVassalId.IsDescendantOf(bob.MedievalVassalId));
context.Vassals.RemoveRange(branch);
context.SaveChanges();
DisplayData(context.Vassals, "All data without Knight Bob branch");

// func for displaying all data
void DisplayData(IEnumerable<MedievalVassal> data, string tableName)
{
    var table = new Table();
    table.AddColumn("HierarchyID");
    table.AddColumn("Name");

    AnsiConsole.Write(new Rule(tableName) {Alignment = Justify.Left});
    foreach (var vassal in data)
    {
        table.AddRow(vassal.MedievalVassalId.ToString(), vassal.Name);
    }

    AnsiConsole.Write(table);
}
