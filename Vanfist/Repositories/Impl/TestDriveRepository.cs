using Microsoft.EntityFrameworkCore;
using Vanfist.Configuration.Database;
using Vanfist.Entities;

namespace Vanfist.Repositories.Impl;

public class TestDriveRepository : ITestDriveRepository
{
    private readonly ApplicationDbContext _db;

    public TestDriveRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(TestDriveRequest e)
    {
        await _db.TestDriveRequests.AddAsync(e);
    }

    public Task<TestDriveRequest?> Get(int id)
    {
        return _db.TestDriveRequests
                  .Include(x => x.Model)
                  .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}
