using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.DTOs;
using MovieWeb.Helpers;
using MovieWeb.Models;
using MovieWeb.Services.Interfaces;

namespace MovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IFilmService _filmService;
        private readonly IMapper mapper;

        public CommentController(ICommentService commentService, 
                                IUserService userService, 
                                IFilmService filmService,
                                IMapper mapper)
        {
            _commentService = commentService;
            _userService = userService;
            _filmService = filmService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetComments(int? size, Guid filmId)
        {
            ICollection<CommentModel> comments = await _commentService.GetComments(size, filmId);
            ICollection<CommentDTO> commentsDTO = new List<CommentDTO>();   
            foreach(var comment in comments)
            {
                commentsDTO.Add(mapper.Map<CommentDTO>(comment));
            }
            return Ok(commentsDTO);

        }

        //POST: api/comment
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CommentModel>>> AddComment([FromBody] CommentDTO comment)
        {
            var email = comment.Email;
            
            if(email == null)
            {
                return BadRequest("You haven't logined, please login before comment!");
            }

            var user = await _userService.GetUser(email);
            if (user == null)
            {
                return BadRequest();
            }

            var film = await _filmService.GetFilm(comment.FilmId);
            if(film == null)
            {
                return BadRequest();
            }

            var commentModel = mapper.Map<CommentModel>(comment);
            commentModel.User = user;
            commentModel.UserId = user.Id;
            commentModel.Film = film;
            commentModel.FilmId = film.Id;

            var isSaved = await _commentService.AddComment(commentModel);
            if(isSaved)
            {
                return CreatedAtAction(nameof(AddComment), mapper.Map<CommentDTO>(commentModel));
            }
            return NoContent();

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, CommentDTO commentDTO)
        {
            var email = SessionHelper.Get<string>(HttpContext.Session, "email");

            if (email == null)
            {
                return BadRequest("You haven't logined, please login before comment!");
            }

            var user = await _userService.GetUser(email);
            if (user == null)
            {
                return BadRequest("This account isn't correct, please logout and login again!");
            }

            var film = await _filmService.GetFilm(commentDTO.FilmId);
            if (film == null)
            {
                return BadRequest("This film isn't exist or removed, please choose other to watch!");
            }

            var commentModel = mapper.Map<CommentModel>(commentDTO);
            commentModel.User = user;
            commentModel.UserId = user.Id;
            commentModel.Film = film;
            commentModel.FilmId = film.Id;

            var isUpdated = await _commentService.UpdateComment(id, commentModel);
            if (isUpdated)
            {
                return Ok(mapper.Map<CommentDTO>(commentModel));
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _commentService.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }

            var isDeleted = await _commentService.DeleteComment(comment);
            if(isDeleted)
            {
                return Ok(mapper.Map<CommentDTO>(comment));
            }
            return NoContent();
        }
    }
}
