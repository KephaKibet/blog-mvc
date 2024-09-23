using blog.Models;

namespace blog.Data.Repository
{
	public interface IRepository
	{
		public Post GetPost(int id);
		List<Post> GetAllPosts();	
		List<Post> GetAllPosts(string Category);	
		void AddPost(Post post);
		void UpdatePost(Post post);	
		void RemovePost(int id);

		Task<bool> SaveChangesAsync();
	}
}
 