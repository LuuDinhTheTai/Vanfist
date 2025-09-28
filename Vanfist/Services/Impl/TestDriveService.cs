using Vanfist.DTOs.Requests;
using Vanfist.DTOs.Responses;
using Vanfist.Entities;
using Vanfist.Repositories;

namespace Vanfist.Services.Impl;

public class TestDriveService : ITestDriveService
{
    private readonly ITestDriveRepository _repo;

    public TestDriveService(ITestDriveRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Create(TestDriveRequestCreate dto)
    {
        var e = new TestDriveRequest
        {
            FullName = dto.FullName.Trim(),
            Email = dto.Email?.Trim(),
            Phone = dto.Phone?.Trim(),
            ModelId = dto.ModelId,
            PreferredTime = dto.PreferredTime,
            Note = dto.Note?.Trim(),
            Status = "New"
        };

        await _repo.Add(e);
        await _repo.SaveChanges();
        return e.Id;
    }

    public async Task<TestDriveRequestItem?> Get(int id)
    {
        var e = await _repo.Get(id);
        if (e == null) return null;

        return new TestDriveRequestItem
        {
            Id = e.Id,
            FullName = e.FullName,
            Phone = e.Phone,
            Email = e.Email,
            ModelName = e.Model?.Name,
            PreferredTime = e.PreferredTime,
            Status = e.Status,
            CreatedAt = e.CreatedAt
        };
    }
}
