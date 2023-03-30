using AniMedia.Application.Common.Interfaces;

namespace AniMedia.IntegrationTests.Mocks;

internal class MockCurrentUserService : ICurrentUserService {

    public Guid? UserUID { get; private set; }

    public void SetUid(Guid? uid) {
        UserUID = uid;
    }
}