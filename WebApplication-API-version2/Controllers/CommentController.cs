using Microsoft.AspNetCore.Mvc;
using WebApplication_API_version2.Interfaces;
using WebApplication_API_version2.DTO;
using WebApplication_API_version2.Mappers;

namespace WebApplication_API_version2.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPersonRepository _personRepo;
        public CommentController(ICommentRepository commentRepo, IPersonRepository personRepo)
        {
            _commentRepo = commentRepo;
            _personRepo = personRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);

        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }



        [HttpPost("{personId:int}")]
        public async Task<IActionResult> Create([FromRoute] int personId,CreateCommentDTO commentDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _personRepo.PersonExists(personId))
            {
                return BadRequest("Person does not exist");
            }
            var commentModel = commentDto.ToCommentFromCreate(personId);
            await _commentRepo.CreateAsync(commentModel);   
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},commentModel.ToCommentDto());

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            if(comment == null)
            {
                return NotFound("Not Found");
            }
            return Ok(comment.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentModel =await _commentRepo.DeleteAsync(id);
            if(commentModel == null)
            {
                return NotFound("Not Found , not exist");
            }
            return Ok(commentModel.ToCommentDto()); 
        }
    }
}
