
namespace GeneratorUsingStoreProcedures.Entities;

public class Personal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GenderId { get; set; }
    public Gender Gender { get; set; }
    public int HobbyId { get; set; }
    public Hobby Hobby { get; set; }
    public int Age { get; set; }
}
