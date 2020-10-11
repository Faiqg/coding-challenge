using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Repository;
using Data.Entites;
using System.Data.Entity;

namespace Logic.Services
{
    public class ScoreService : IScoreService
    {
        private readonly LGAContext _context;
		private readonly StateService _stateService;
        public ScoreService(LGAContext context)
        {
            _context = context;
			_stateService = new StateService(context);
        }

		/// <summary>
		/// Get all data for given state ID
		/// </summary>
		/// <param name="stateId">State ID</param>
		/// <returns>Scores for the state</returns>
		public IEnumerable<ScoresModel> GetAllData(int? stateId)
		{
			// Default state
			if (!stateId.HasValue)
			{
				stateId = _stateService.VicStateId;
			}

			var data =
				_context.Scores
				.Where(p => p.Location.State.StateId == stateId)
				.Join(_context.Locations,
				s => s.Location.LocationId,
				l => l.LocationId,
				(s, l) => new
				{
					l.State.StateId,
					l.PlaceName,
					s.AdvantageDisadvantageScore,
					s.DisadvantageScore,
					s.Year
				}).Join(_context.States,
				s => s.StateId,
				st => st.StateId,
				(s, st) => new ScoresModel()
				{
					StateName = st.StateName,
					PlaceName = s.PlaceName,
					Year = s.Year,
					AdvantageDisadvantageScore = s.AdvantageDisadvantageScore,
					DisadvantageScore = s.DisadvantageScore
				})
				.OrderBy(p => new { p.StateName, p.PlaceName })
				.ToList();
			return data;
		}

		/// <summary>
		/// Get score for the state for given year
		/// The score should be higher than state's/territory's median
		/// </summary>
		/// <param name="stateId">State ID</param>
		/// <param name="year">Year</param>
		/// <returns>Scores</returns>
		public IEnumerable<Score> GetScores(int? stateId, int? year)
        {
			var scores = _context.Scores
					.Where(p => p.Year == year
						&& p.Location.State.StateId == stateId
						&& p.Location.State.Median < p.DisadvantageScore)
					.Include(x=>x.Location)
					.Include(y=>y.Location.State)
					.ToList();
			return scores;
		}
	}
}
