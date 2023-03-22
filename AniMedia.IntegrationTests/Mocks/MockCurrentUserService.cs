using AniMedia.Application.Common.Interfaces;

namespace AniMedia.IntegrationTests.Mocks;

internal class MockCurrentUserService : ICurrentUserService {
    private Guid? _uid = null;

    public Guid? UserUID => _uid;

    public void SetUid(Guid? uid) => _uid = uid;
}