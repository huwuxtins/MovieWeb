using MovieWeb.Models;

namespace MovieWeb.Services.Interfaces
{
    public interface IFilmService
    {
        public Task<ICollection<FilmModel>> GetFilms(int size, int? page, string? sort, string? sortedProperty, string? name, string? search);
        public Task<ICollection<FilmModel>> GetFilmsAdmin(int size, int? page, string? sort, string? sortedProperty, string? name, string? search);
        public Task<FilmModel> GetFilm(Guid id);
        public Task<bool> AddFilm(FilmModel filmModel);
        public Task<bool> UpdateFilm(Guid id, FilmModel filmModel);
        public Task<bool> DeleteFilm(FilmModel filmModel);

    }
}
