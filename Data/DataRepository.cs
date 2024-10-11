using System.Data;
using GeneratorUsingStoreProcedures.DTOs;
using GeneratorUsingStoreProcedures.Interfaces;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GeneratorUsingStoreProcedures.Data;

public class DataRepository : IDataRepository
{
    private readonly DataGeneratorContext _context;
    private readonly IMapper _mapper;

    public DataRepository(DataGeneratorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PersonalDto>> GetAllPersonalsAsync()
    {
        var personals = await _context.Personals
            .Include(p => p.Gender)
            .Include(p => p.Hobby)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PersonalDto>>(personals);
    }

    public async Task AddPersonalRangeAsync(IEnumerable<PersonalDto> personals)
    {
        await ClearAllDataAsync(); // Clear data dulu sebelum simpan data baru

        var dataTable = new DataTable();
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("GenderName", typeof(string));
        dataTable.Columns.Add("HobbyName", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));

        foreach (var personal in personals)
        {
            dataTable.Rows.Add(personal.Name, personal.GenderName, personal.HobbyName, personal.Age);
        }

        var parameter = new SqlParameter("@PersonalData", SqlDbType.Structured)
        {
            TypeName = "PersonalTableType",
            Value = dataTable
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC InsertPersonalData @PersonalData", parameter);
    }

    public async Task ClearAllDataAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE tblT_personal");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM tblM_gender");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM tblM_hobby");
    }

    public async Task<IEnumerable<GenderTotalDto>> GetGenderTotalsAsync()
    {
        return await _context.Set<GenderTotalDto>().FromSqlRaw("EXEC GetGenderTotals").ToListAsync();
    }
}
