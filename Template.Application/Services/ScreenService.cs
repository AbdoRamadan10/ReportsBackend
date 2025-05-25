using AutoMapper;
using ReportsBackend.Application.DTOs.Role;
using ReportsBackend.Application.DTOs.Screen;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Services
{
    public class ScreenService
    {
        private readonly IGenericRepository<Screen> _screenRepository;
        private readonly IMapper _mapper;

        public ScreenService(IGenericRepository<Screen> screenRepository, IMapper mapper)
        {
            _screenRepository = screenRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ScreenDto>> GetAllAsync(FindOptions options)
        {
            var screens = await _screenRepository.GetAllAsync(options);
            return new PaginatedResult<ScreenDto>
            {
                Items = _mapper.Map<IEnumerable<ScreenDto>>(screens.Items),
                PageNumber = screens.PageNumber,
                PageSize = screens.PageSize,
                TotalCount = screens.TotalCount,

            };
        }

        //public async Task<ScreenDto> GetByIdAsync(int id)
        //{
        //    var screen = await _screenRepository.GetByIdAsync(id);
        //    return _mapper.Map<ScreenDto>(screen);
        //}

        public async Task<ScreenDto> CreateAsync(ScreenCreateDto dto)
        {
            var screen = _mapper.Map<Screen>(dto);
            await _screenRepository.AddAsync(screen);
            return _mapper.Map<ScreenDto>(screen);
        }

        //public async Task UpdateAsync(int id, ScreenUpdateDto dto)
        //{
        //    var screen = await _screenRepository.GetByIdAsync(id);
        //    if (screen == null) throw new KeyNotFoundException();
        //    _mapper.Map(dto, screen);
        //    await _screenRepository.Update(screen);
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var screen = await _screenRepository.GetByIdAsync(id);
        //    if (screen == null) throw new KeyNotFoundException();
        //    await _screenRepository.Delete(screen);
        //}
    }
}
