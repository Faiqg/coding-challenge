using System.Collections.Generic;
using Data.Entites;
using Data.ViewModel;

namespace Logic.Services
{
    public interface IStateService
    {
        IEnumerable<StateModel> GetAllStates();
        StateModel GetState(int stateId);
        int GetStateId(string stateName);
        void AddState(State state);
    }
}
