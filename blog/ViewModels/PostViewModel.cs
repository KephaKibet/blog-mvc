using Microsoft.AspNetCore.Http;

namespace blog.ViewModels
{
	public class PostViewModel
	{

		public int Id { get; set; }
		public string Tittle { get; set; } = "";
		public string Body { get; set; } = "";
		public IFormFile Image { get; set; } = null;

	}
}
