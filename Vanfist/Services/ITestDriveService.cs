using Vanfist.DTOs.Requests;
using Vanfist.DTOs.Responses;

namespace Vanfist.Services;

public interface ITestDriveService
{
    Task<int> Create(TestDriveRequestCreate dto);
    Task<TestDriveRequestItem?> Get(int id);
}
