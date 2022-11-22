using Microsoft.EntityFrameworkCore;

namespace Task2.Console;

public class MedievalVassal
{
    public MedievalVassal(HierarchyId MedievalVassalId, string Name)
    {
        this.MedievalVassalId = MedievalVassalId;
        this.Name = Name;
    }
    public HierarchyId MedievalVassalId { get; set; }
    public string Name { get; set; }
}