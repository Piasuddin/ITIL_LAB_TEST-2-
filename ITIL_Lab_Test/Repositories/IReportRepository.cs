using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Repositories
{
    public interface IReportRepository
    {
        Task<ReportViewModel> GetReportByOrderId(long id);
    }
}
