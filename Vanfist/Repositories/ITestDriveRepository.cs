using Vanfist.Entities;

namespace Vanfist.Repositories;

public interface ITestDriveRepository
{
    Task Add(TestDriveRequest e);
    Task<TestDriveRequest?> Get(int id);
    Task SaveChanges();
}
