using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportsBackend.Application.DTOs.Report;
using ReportsBackend.Application.DTOs.ReportColumn;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Exceptions;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;
using System.Data;

namespace ReportsBackend.Application.Services
{
    public class ReportService
    {
        private readonly IGenericRepository<Report> _reportRepository;
        private readonly IGenericRepository<ReportColumn> _reportColumnRepository;
        private readonly IMapper _mapper;

        public ReportService(IGenericRepository<Report> reportRepository, IMapper mapper, IGenericRepository<ReportColumn> reportColumnRepository)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportColumnRepository = reportColumnRepository;
        }

        public async Task<PaginatedResult<ReportDto>> GetAllAsync(FindOptions options)
        {
            var reports = await _reportRepository.GetAllAsync(options, q => q.Include(t => t.Privilege));
            var activeReports = reports.Items.Where(r => r.Active && !r.Hide);
            return new PaginatedResult<ReportDto>
            {
                Items = _mapper.Map<IEnumerable<ReportDto>>(activeReports),
                PageNumber = reports.PageNumber,
                PageSize = reports.PageSize,
                TotalCount = reports.TotalCount,

            };
        }

        public async Task<ReportDto> GetByIdAsync(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id, q => q.Include(t => t.Privilege), q => q.Include(t => t.Parameters), q => q.Include(t => t.Columns));
            if (report == null)
                throw new NotFoundException("Report", id.ToString());
            return _mapper.Map<ReportDto>(report);
        }

        public async Task<ReportDto> GetByNameAsync(string name)
        {
            var reports = await _reportRepository.FindAsync(r => r.Name == name, q => q.Include(t => t.Privilege), q => q.Include(t => t.Parameters), q => q.Include(t => t.Columns));
            var report = reports.FirstOrDefault();
            if (report == null)
                throw new NotFoundException("Report", name);
            return _mapper.Map<ReportDto>(report);
        }

        public async Task<List<string>> GetDashboardNames()
        {
            var reports = await _reportRepository.FindAsync(r => r.Category == "Dashboard" && r.Active);
            return reports.Select(r => r.Name).ToList();
        }



        public async Task<ReportDto> CreateAsync(ReportCreateDto dto)
        {
            var report = _mapper.Map<Report>(dto);
            await _reportRepository.AddAsync(report);
            return _mapper.Map<ReportDto>(report);
        }

        public async Task UpdateAsync(int id, ReportUpdateDto dto)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
                throw new NotFoundException("Report", id.ToString());
            _mapper.Map(dto, report);
            await _reportRepository.Update(report);
        }

        public async Task DeleteAsync(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
                throw new NotFoundException("Report", id.ToString());
            await _reportRepository.Delete(report);
        }


        public async Task<List<ReportColumnDto>> GetColumnsByReportIdAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId, q => q.Include(t => t.Columns));
            if (report == null)
                throw new NotFoundException("Report", reportId.ToString());

            var columns = await _reportColumnRepository.GetAllAsync(new FindOptions { });
            return _mapper.Map<List<ReportColumnDto>>(columns.Items);
        }

        public async Task<ReportColumnDto> GetColumnByIdAsync(int id)
        {
            var column = await _reportColumnRepository.GetByIdAsync(id);
            if (column == null)
                throw new NotFoundException("ReportColumn", id.ToString());
            return _mapper.Map<ReportColumnDto>(column);

        }
        public async Task<List<ReportColumnDto>> CreateColumnsAsync(int reportId, List<ReportColumnCreateDto> reportColumns)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            if (report == null)
                throw new NotFoundException("Report", reportId.ToString());

            foreach (var columnDto in reportColumns)
            {
                var column = _mapper.Map<ReportColumn>(columnDto);
                column.ReportId = reportId;
                await _reportColumnRepository.AddAsync(column);
            }


            var columns = _mapper.Map<List<ReportColumnDto>>(reportColumns);
            return columns;

        }

        public async Task<ReportColumnDto> UpdateColumnAsync(int reportId, int columnId, ReportColumnCreateDto dto)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            if (report == null)
                throw new NotFoundException("Report", reportId.ToString());
            var column = await _reportColumnRepository.GetByIdAsync(columnId);
            if (column == null)
                throw new NotFoundException("ReportColumn", columnId.ToString());
            _mapper.Map(dto, column);
            await _reportColumnRepository.Update(column);
            return _mapper.Map<ReportColumnDto>(column);
        }


    }
}
