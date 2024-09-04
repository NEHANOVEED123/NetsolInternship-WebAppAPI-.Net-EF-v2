using WebApplication_API_version2.Models;

namespace WebApplication_API_version2.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id,Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
        
    }
}
