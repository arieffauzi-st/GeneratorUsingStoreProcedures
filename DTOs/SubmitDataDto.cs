using System;

namespace GeneratorUsingStoreProcedures.DTOs;

public class SubmitDataDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public List<PersonalDto> Payload { get; set; }
}
