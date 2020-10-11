
using System.Collections.Generic;
using Data.ViewModel;

namespace Logic.Services
{
    public interface IReportService
    {
        IEnumerable<ReportModel> GetDisadges(ReportSearchModel searchParams);
    }
}
