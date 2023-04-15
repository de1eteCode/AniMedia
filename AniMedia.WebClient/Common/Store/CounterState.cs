using Fluxor;

namespace AniMedia.WebClient.Common.Store; 

[FeatureState]
public class CounterState {
    public int Count { get; init; }

    private CounterState() { }
    
    public CounterState(int count) {
        Count = count;
    }
}

public class IncrementCounterAction {
    public int IncrementSize { get; init; }
}

public static class CounterReducers {
    [ReducerMethod]
    public static CounterState OnIncrementAction(CounterState state, IncrementCounterAction action) {
        return new CounterState(state.Count + action.IncrementSize);
    }
}