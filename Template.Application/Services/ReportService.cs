using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportsBackend.Application.DTOs.Report;
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
        private readonly IMapper _mapper;

        public ReportService(IGenericRepository<Report> reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ReportDto>> GetAllAsync(FindOptions options)
        {
            var reports = await _reportRepository.GetAllAsync(options, q => q.Include(t => t.Privilege));
            return new PaginatedResult<ReportDto>
            {
                Items = _mapper.Map<IEnumerable<ReportDto>>(reports.Items),
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



    }
}
