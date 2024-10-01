using App.DataTransferObject;
using App.Models;
using App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReportDto>> GetReportAsync()
        {
            return await _repository.GetReportAsync();
        }
    }
}
