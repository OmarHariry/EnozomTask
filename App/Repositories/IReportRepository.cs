using App.DataTransferObject;

namespace App.Repositories
{
    public interface IReportRepository
    {
        public Task<IEnumerable<ReportDto>> GetReportAsync();
    }
}
