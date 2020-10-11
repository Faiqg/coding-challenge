using Data.Entites;
using Data.ViewModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services
{
    public class StateService : IStateService
    {
        private readonly LGAContext _context;
        private int _VicStateId = -1;
        public StateService(LGAContext context)
        {
            _context = context;
        }

        public int VicStateId
        {
            get
            {
                if (_VicStateId == -1)
                {
                    var state = _context.States.Single(p => p.StateName.Equals("Victoria", StringComparison.CurrentCultureIgnoreCase));
                    _VicStateId = state != null ? state.StateId : -1;
                }
                return _VicStateId;
            }
        }
        public IEnumerable<StateModel> GetAllStates()
        {
            return MapStates(_context.States.ToList());
        }

        public StateModel GetState(int stateId)
        {
            return MapAState(_context.States.Where(s => s.StateId == stateId).FirstOrDefault());
        }

        public int GetStateId(string stateName)
        {
            throw new NotImplementedException();
        }

        public void AddState(State state)
        {
            _context.States.Add(state);
            _context.SaveChanges();
        }

        /// <summary>
        /// To map db model State with view model State.
        /// Must be replaced by AutoMapper library
        /// </summary>
        /// <param name="dbStates"></param>
        /// <returns></returns>
        private IEnumerable<StateModel> MapStates(IEnumerable<State> dbStates)
        {
            var statesList = new List<StateModel>();
            foreach (var state in dbStates)
            {
                
                statesList.Add(MapAState(state));
            }
            return statesList;
        }
        private StateModel MapAState(State dbState)
        {
            var stateModel = new StateModel
            {
                StateId = dbState.StateId,
                StateName = dbState.StateName,
                Median = dbState.Median
            };
            return stateModel;
        }
    }
}