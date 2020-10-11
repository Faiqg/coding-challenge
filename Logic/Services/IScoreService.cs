using Data.Entites;
using Data.ViewModel;
using System.Collections.Generic;

namespace Logic.Services
{
    public interface IScoreService
    {
        IEnumerable<ScoresModel> GetAllData(int? stateId);
        IEnumerable<Score> GetScores(int? stateId, int? year);
    }
}
