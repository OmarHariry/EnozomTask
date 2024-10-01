using App.DataTransferObject;
using App.Models;
using App.Repositories;
using App.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportDto>> GetReportAsync()
        {
            return await _unitOfWork.Reports.GetReportAsync();
        }
    }
}
