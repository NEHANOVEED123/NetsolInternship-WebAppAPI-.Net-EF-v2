using System.Runtime.CompilerServices;
using WebApplication_API_version2.Models;
using WebApplication_API_version2.DTO;

namespace WebApplication_API_version2.Mappers
{
    public static class CommentMappers
    {
        public static CommentDTO ToCommentDto(this Comment commentModel )
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                PersonId = commentModel.PersonId,
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDTO commentDto,int personId )
        {
            return new Comment
            {

                Title = commentDto.Title,
                Content = commentDto.Content,
                PersonId = personId,
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentDTO commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content

            };

        }

    }
    
}
