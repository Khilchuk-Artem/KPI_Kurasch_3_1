using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class VisitorCardRepository : IVisitorCardRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public VisitorCardRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<VisitorCardDTO> GetVisitorCardByIdAsync(int id)
        {
            var visitorCard = await _dbContext.VisitorsCards
                .Where(vc => !vc.IsDeleted)
                .FirstOrDefaultAsync(vc => vc.Id == id);

            if (visitorCard == null)
            {
                throw new InvalidOperationException($"Visitor card with id {id} not found.");
            }

            return MapToDto(visitorCard);
        }

        public async Task<List<VisitorCardDTO>> GetVisitorCardsAsync()
        {
            var visitorCards = await _dbContext.VisitorsCards
                .Where(vc => !vc.IsDeleted)
                .ToListAsync();

            return visitorCards.Select(vc => MapToDto(vc)).ToList();
        }

        public async Task<List<VisitorCardShortcutDTO>> GetVisitorCardShortcutsAsync(int pageSize, int pageNumber, string query)
        {
            var visitorCards = await _dbContext.VisitorsCards
                .Where(vc => !vc.IsDeleted && (vc.Name.ToLower() + " " + vc.Patronymic.ToLower() + " " + vc.Surname.ToLower()).Contains(query.ToLower()))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<VisitorCardShortcutDTO>>(visitorCards);
        }

        public async Task<int> CreateVisitorCardAsync(CreateVisitorCardDTO dto)
        {
            var visitorCard = _mapper.Map<VisitorsCard>(dto);
            await _dbContext.VisitorsCards.AddAsync(visitorCard);
            await _dbContext.SaveChangesAsync();

            return visitorCard.Id;
        }

        public async Task<VisitorCardDTO?> UpdateVisitorCardAsync(CreateVisitorCardDTO dto, int id)
        {
            var visitorCard = await _dbContext.VisitorsCards
                .Where(vc => !vc.IsDeleted)
                .FirstOrDefaultAsync(vc => vc.Id == id);

            if (visitorCard == null)
            {
                throw new InvalidOperationException($"Visitor card with id {id} not found.");
            }

            _mapper.Map(dto, visitorCard);
            await _dbContext.SaveChangesAsync();

            return MapToDto(visitorCard);
        }

        public async Task<VisitorCardDTO?> DeleteVisitorCardAsync(int id)
        {
            var visitorCard = await _dbContext.VisitorsCards
                .Where(vc => !vc.IsDeleted)
                .FirstOrDefaultAsync(vc => vc.Id == id);

            if (visitorCard == null)
            {
                throw new InvalidOperationException($"Visitor card with id {id} not found.");
            }

            visitorCard.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return MapToDto(visitorCard);
        }

        private VisitorCardDTO MapToDto(VisitorsCard visitorCard)
        {
            return _mapper.Map<VisitorCardDTO>(visitorCard);
        }
    }
}
