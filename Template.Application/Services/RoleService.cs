using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportsBackend.Application.DTOs.Report;
using ReportsBackend.Application.DTOs.Role;
using ReportsBackend.Application.DTOs.Screen;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Exceptions;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Services
{
    public class RoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Screen> _screenRepository;
        private readonly IGenericRepository<Report> _reportRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository,
        IGenericRepository<Screen> screenRepository,
        IGenericRepository<Report> reportRepository,
        IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _screenRepository = screenRepository;
            _reportRepository = reportRepository;
        }

        public async Task<PaginatedResult<RoleDto>> GetAllAsync(FindOptions options)
        {
            var roles = await _roleRepository.GetAllAsync(options);
            return new PaginatedResult<RoleDto>
            {
                Items = _mapper.Map<IEnumerable<RoleDto>>(roles.Items),
                PageNumber = roles.PageNumber,
                PageSize = roles.PageSize,
                TotalCount = roles.TotalCount,

            };
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new NotFoundException("Role", id.ToString());
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateAsync(RoleCreateDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            await _roleRepository.AddAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task UpdateAsync(int id, RoleUpdateDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new NotFoundException("Role", id.ToString());
            _mapper.Map(dto, role);
            await _roleRepository.Update(role);
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new NotFoundException("Role", id.ToString());
            await _roleRepository.Delete(role);
        }


  public async Task<RoleAccessDto> GetReportsAndScreensAsync(int roleId)
        {
            // Get the role with related screens and reports by roleId
            var role = await _roleRepository.GetByIdAsync(
                roleId,
                q => q.Include(r => r.RoleScreens).ThenInclude(rs => rs.Screen)
                      .Include(r => r.RoleReports).ThenInclude(rr => rr.Report).ThenInclude(r => r.Privilege)
            );

            if (role == null)
                throw new NotFoundException("Role", roleId.ToString());

            var screens = role.RoleScreens?.Select(rs => rs.Screen) ?? Enumerable.Empty<Screen>();
            var reports = role.RoleReports?.Select(rr => rr.Report) ?? Enumerable.Empty<Report>();

            return new RoleAccessDto
            {
                Screens = _mapper.Map<IEnumerable<ScreenDto>>(screens),
                Reports = _mapper.Map<IEnumerable<ReportDto>>(reports)
            };
        }
    }
}
