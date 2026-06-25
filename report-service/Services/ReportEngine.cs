using System;
using System.Collections.Generic;
using report_service.DTO;

namespace report_service.Services
{
    public class ReportEngine : IReportEngine
    {
        public ReportSummaryDto GenerateSummary(List<TransactionDto> transactions)
        {
            decimal totalIncome = 0;
            decimal totalExpense = 0;

            foreach (var transaction in transactions)
            {
                if (string.Equals(transaction.Type, "INCOME", StringComparison.OrdinalIgnoreCase))
                {
                    totalIncome += transaction.Amount;
                }
                else if (string.Equals(transaction.Type, "EXPENSE", StringComparison.OrdinalIgnoreCase))
                {
                    totalExpense += transaction.Amount;
                }
            }

            decimal balance = totalIncome - totalExpense;

            return new ReportSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance
            };
        }
    }
}
