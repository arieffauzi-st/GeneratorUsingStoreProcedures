using System;

namespace GeneratorUsingStoreProcedures.Entities;

public class Hobby
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Personal> Personals { get; set; }
}
