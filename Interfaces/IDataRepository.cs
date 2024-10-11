
using GeneratorUsingStoreProcedures.DTOs;

namespace GeneratorUsingStoreProcedures.Interfaces;

public interface IDataRepository
{
    Task<IEnumerable<PersonalDto>> GetAllPersonalsAsync();
    Task AddPersonalRangeAsync(IEnumerable<PersonalDto> personals);
    Task ClearAllDataAsync();
    Task<IEnumerable<GenderTotalDto>> GetGenderTotalsAsync();
}
