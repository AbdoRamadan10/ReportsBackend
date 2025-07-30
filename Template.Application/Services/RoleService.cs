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
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository,
        IGenericRepository<Screen> screenRepository,
        IGenericRepository<Report> reportRepository,
        IGenericRepository<UserRole> userRoleRepository,
        IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _screenRepository = screenRepository;
            _reportRepository = reportRepository;
            _userRoleRepository = userRoleRepository;


        }

        public async Task<PaginatedResult<RoleDto>> GetAllAsync(FindOptions options)
        {
            var roles = await _roleRepository.GetAllAsync(options
                ,r=>r.Include(report=>report.RoleReports)
                ,r => r.Include(screen => screen.RoleScreens)
                ,r => r.Include(report => report.RoleReports).ThenInclude(s=>s.Report)
                ,r => r.Include(screen => screen.RoleScreens).ThenInclude(s=>s.Screen)
                );
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
            var role = await _roleRepository.GetByIdAsync(id
                , r => r.Include(report => report.RoleReports)
                , r => r.Include(screen => screen.RoleScreens)
                , r => r.Include(report => report.RoleReports).ThenInclude(s => s.Report)
                , r => r.Include(screen => screen.RoleScreens).ThenInclude(s => s.Screen)
                );
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


        public async Task<RoleAccessDto> GetUserReportsAndScreensAsync(int userId)
        {
            // Get all UserRole entries for the user, including related Role, RoleScreens, RoleReports, and their entities
            var userRolesResult = await _userRoleRepository.GetAllAsync(
                new FindOptions { },
                q => q.Include(ur => ur.Role)
                      .ThenInclude(r => r.RoleScreens)
                          .ThenInclude(rs => rs.Screen)
                      .Include(ur => ur.Role)
                      .ThenInclude(r => r.RoleReports)
                          .ThenInclude(rr => rr.Report)
                              .ThenInclude(rp => rp.Privilege)
            );

            var roles = userRolesResult.Items
                .Where(ur => ur.UserId == userId && ur.Role != null)
                .Select(ur => ur.Role)
                .ToList();

            var screens = roles
                .SelectMany(r => r.RoleScreens ?? Enumerable.Empty<RoleScreen>())
                .Select(rs => rs.Screen)
                .Where(s => s != null)
                .GroupBy(s => s.Id)
                .Select(g => g.First())
                .ToList();

            var reports = roles
                .SelectMany(r => r.RoleReports ?? Enumerable.Empty<RoleReport>())
                .Select(rr => rr.Report)
                .Where(rp => rp != null)
                .GroupBy(rp => rp.Id)
                .Select(g => g.First())
                .ToList();

            return new RoleAccessDto
            {
                Screens = _mapper.Map<IEnumerable<ScreenDto>>(screens),
                Reports = _mapper.Map<IEnumerable<ReportDto>>(reports)
            };
        }

        public async Task AssignScreenToRoleAsync(int roleId, List<int> screenIds)
        {
            var role = await _roleRepository.GetByIdAsync(roleId, r => r.Include(rs => rs.RoleScreens));
            if (role == null)
                throw new NotFoundException("Role", roleId.ToString());
            // Remove existing screens
            var existingScreens = role.RoleScreens?.Select(rs => rs.ScreenId).ToList() ?? new List<int>();
            foreach (var screenId in existingScreens)
            {
                if (!screenIds.Contains(screenId))
                {
                    var roleScreen = role.RoleScreens.FirstOrDefault(rs => rs.ScreenId == screenId);
                    if (roleScreen != null)
                    {
                        role.RoleScreens.Remove(roleScreen);
                    }
                }
            }
            // Add new screens
            foreach (var screenId in screenIds)
            {
                if (!existingScreens.Contains(screenId))
                {
                    var screen = await _screenRepository.GetByIdAsync(screenId);
                    if (screen != null)
                    {
                        role.RoleScreens.Add(new RoleScreen { Screen = screen });
                    }
                }
            }
            await _roleRepository.Update(role);
        }

        public async Task AssignReportToRoleAsync(int roleId, List<int> reportIds)
        {
            var role = await _roleRepository.GetByIdAsync(roleId, r => r.Include(rr => rr.RoleReports));
            if (role == null)
                throw new NotFoundException("Role", roleId.ToString());
            // Remove existing reports
            var existingReports = role.RoleReports?.Select(rr => rr.ReportId).ToList() ?? new List<int>();
            foreach (var reportId in existingReports)
            {
                if (!reportIds.Contains(reportId))
                {
                    var roleReport = role.RoleReports.FirstOrDefault(rr => rr.ReportId == reportId);
                    if (roleReport != null)
                    {
                        role.RoleReports.Remove(roleReport);
                    }
                }
            }
            // Add new reports
            foreach (var reportId in reportIds)
            {
                if (!existingReports.Contains(reportId))
                {
                    var report = await _reportRepository.GetByIdAsync(reportId);
                    if (report != null)
                    {
                        role.RoleReports.Add(new RoleReport { Report = report });
                    }
                }
            }
            await _roleRepository.Update(role);


        }
    }
}
