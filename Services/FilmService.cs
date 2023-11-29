using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieWeb.DTOs;
using MovieWeb.Models;
using MovieWeb.Services.Interfaces;

namespace MovieWeb.Services
{
    public class FilmService : IFilmService
    {
        private readonly int pageSize = 10;

        private readonly MovieDbContext _dbContext;
        public FilmService(MovieDbContext _context)
        {
            _dbContext = _context;
        }
        public async Task<ICollection<FilmModel>> GetFilms(int size, int? page, string? sort, string? sortedProperty, string? name, string? search)
        {
            ICollection<FilmModel> films = await _dbContext.FilmModels.AsNoTracking().ToListAsync();
            if (page.HasValue)
            {
                films = films.Skip((page.Value - 1) * pageSize).ToList();
            }

            if (!sort.IsNullOrEmpty() && !sortedProperty.IsNullOrEmpty())
            {
                if (sort.Equals("asc"))
                {
                    films = films.OrderBy(f => f.Name).ToList();
                }
            }

            if (!name.IsNullOrEmpty())
            {
                films = films.Where(film => film.Genre.Equals(name)).ToList();
            }

            if (!search.IsNullOrEmpty())
            {
                films = films.Where(film => film.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            films = films.OrderBy(film => Guid.NewGuid()).Take(size).ToList();
            return films;
        }

        public async Task<ICollection<FilmModel>> GetFilmsAdmin(int size, int? page, string? sort, string? sortedProperty, string? name, string? search)
        {
            ICollection<FilmModel> films = await _dbContext.FilmModels.AsNoTracking().ToListAsync();
            if (page.HasValue)
            {
                films = films.Skip((page.Value - 1) * pageSize).ToList();
            }

            if (!sort.IsNullOrEmpty() && !sortedProperty.IsNullOrEmpty())
            {
                if (sort.Equals("asc"))
                {
                    films = films.OrderBy(f => f.Name).ToList();
                }
            }

            if (!name.IsNullOrEmpty())
            {
                films = films.Where(film => film.Genre.Equals(name)).ToList();
            }

            if (!search.IsNullOrEmpty())
            {
                films = films.Where(film => film.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            films = films.Take(size).ToList();
            return films;
        }

        public async Task<FilmModel> GetFilm(Guid id)
        {
            return await _dbContext.FilmModels.FirstOrDefaultAsync(film => film.Id == id);
        }

        public async Task<bool> AddFilm(FilmModel filmModel)
        {
            _dbContext.FilmModels.Add(filmModel);
            var isSaved = await _dbContext.SaveChangesAsync();
            return isSaved > 0;
        }

        public async Task<bool> UpdateFilm(Guid id, FilmModel filmModel)
        {
            var isUpdated = await _dbContext.SaveChangesAsync();
            return isUpdated > 0;
        }

        public async Task<bool> DeleteFilm(FilmModel filmModel)
        {
            _dbContext.FilmModels.Remove(filmModel);
            var isDeleted = await _dbContext.SaveChangesAsync();
            return isDeleted > 0;
        }
    }
}
