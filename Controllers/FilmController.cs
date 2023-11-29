using Microsoft.AspNetCore.Mvc;
using MovieWeb.Models;
using MovieWeb.DTOs;
using AutoMapper;
using MovieWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IMapper mapper;
        public FilmController(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmModel>>> GetFilms(int size, int? page, string? sort, string? sortedProperty, string? name, string? search)
        {
            ICollection<FilmModel> films = await _filmService.GetFilms(size, page, sort, sortedProperty, name, search);
            ICollection<FilmDTO> filmsDTO = new List<FilmDTO>();
            foreach(var film in films)
            {
                filmsDTO.Add(mapper.Map<FilmDTO>(film));
            }
            return Ok(filmsDTO);
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpGet("/api/film/admin")]
        public async Task<ActionResult<IEnumerable<FilmModel>>> GetFilmsAdmin(int size, int? page, string? sort, string? sortedProperty, string? name, string? search)
        {
            ICollection<FilmModel> films = await _filmService.GetFilmsAdmin(size, page, sort, sortedProperty, name, search);
            ICollection<FilmDTO> filmsDTO = new List<FilmDTO>();
            foreach (var film in films)
            {
                filmsDTO.Add(mapper.Map<FilmDTO>(film));
            }
            return Ok(filmsDTO);
        }

        //Get film by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(Guid id)
        {
            FilmModel film = await _filmService.GetFilm(id);
            if(film != null)
            {
                return Ok(mapper.Map<FilmDTO>(film));
            }
            return NotFound();
        }

        //POST: api/user
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<FilmModel>>> AddFilm([FromBody] FilmModel filmModel)
        {
            var isSaved = await _filmService.AddFilm(filmModel);
            if (isSaved)
            {
                return CreatedAtAction(nameof(AddFilm), mapper.Map<FilmDTO>(filmModel));
            }
            return NoContent();
        }

        [HttpPut("")]
        public async Task<ActionResult> ReviewFilm(ReviewDTO reviewDTO)
        {
            var film = await _filmService.GetFilm(reviewDTO.Id);
            if (film == null)
            {
                return NotFound();
            }
            film.Reviews++;
            film.Rating += reviewDTO.Score;
            var isUpdated = await _filmService.UpdateFilm(reviewDTO.Id, film);
            if (isUpdated)
            {
                return Ok(mapper.Map<FilmDTO>(film));
            }
            return NoContent();
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(Guid id, FilmModel filmModel)
        {   
            var film = await _filmService.GetFilm(id);
            if (film == null)
            {
                return NotFound();
            }
            mapper.Map(filmModel, film);
            var isUpdated = await _filmService.UpdateFilm(id, film);
            if (isUpdated)  
            {
                return Ok(mapper.Map<FilmDTO>(film));
            }
            return NoContent();
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(Guid id)
        {
            var film = await _filmService.GetFilm(id);
            if (film == null)
            {
                return NotFound();
            }
            var isDeleted = await _filmService.DeleteFilm(film);
            if (isDeleted)
            {
                return Ok(mapper.Map<FilmDTO>(film));
            }
            return NoContent();
        }
    }
}
