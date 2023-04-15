using AniMedia.WebClient.Common.ApiServices;
using Fluxor;

namespace AniMedia.WebClient.Common.Store;

[FeatureState]
public class UserInfoState {

    public string? UserName { get; init; }

    public string? FirstName { get; init; }

    public string? SecondName { get; init; }

    public Guid? AvatarUid { get; init; }

    public bool Loading { get; init; }
    
    public UserInfoState() {
        
    }
}

public static class UserInfoActions {

    public class LoadUserInfo { }

    public class SetNewAvatar {

        public required Guid Uid { get; init; }
    }

    public class SetNewPersonality {

        public required string FirstName { get; init; }

        public required string SecondName { get; init; }
    }

    public class SetNewState {
        public string? UserName { get; init; }

        public string? FirstName { get; init; }

        public string? SecondName { get; init; }

        public Guid? AvatarUid { get; init; }
    }
}

public static class UserInfoReducers {

    [ReducerMethod]
    public static UserInfoState OnActionSet(UserInfoState currentState, UserInfoActions.SetNewAvatar action) {
        return new UserInfoState {
            AvatarUid = action.Uid,
            FirstName = currentState.FirstName,
            SecondName = currentState.SecondName,
            UserName = currentState.UserName
        };
    }

    [ReducerMethod]
    public static UserInfoState OnActionSet(UserInfoState currentState, UserInfoActions.SetNewPersonality action) {
        return new UserInfoState {
            AvatarUid = currentState.AvatarUid,
            FirstName = action.FirstName,
            SecondName = action.SecondName,
            UserName = currentState.UserName
        };
    }

    [ReducerMethod]
    public static UserInfoState OnActionSet(UserInfoState currentState, UserInfoActions.SetNewState action) {
        return new UserInfoState {
            AvatarUid = action.AvatarUid,
            FirstName = action.FirstName,
            SecondName = action.SecondName,
            UserName = action.UserName
        };
    }
}

public class UserInfoEffects {

    private readonly IApiClient _apiClient;

    public UserInfoEffects(IApiClient client) {
        _apiClient = client;
    }

    [EffectMethod(typeof(UserInfoActions.LoadUserInfo))]
    public async Task LoadUserInfo(IDispatcher dispatcher) {
        var result = await _apiClient.ApiV1AccountProfileAsync();
        UserInfoActions.SetNewState newState = default!;

        if (result != null) {
            newState = new UserInfoActions.SetNewState() {
                AvatarUid = result.Avatar?.UID,
                FirstName = result.FirstName,
                SecondName = result.SecondName,
                UserName = result.NickName
            };
        }
        else {
            newState = new UserInfoActions.SetNewState() {
                AvatarUid = null,
                FirstName = string.Empty,
                SecondName = string.Empty,
                UserName = string.Empty
            };
        }

        dispatcher.Dispatch(newState);
    }
}