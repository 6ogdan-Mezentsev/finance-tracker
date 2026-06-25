using System.Collections.Generic;
using report_service.DTO;

namespace report_service.Services
{
    public interface IReportEngine
    {
        ReportSummaryDto GenerateSummary(List<TransactionDto> transactions);
    }
}
