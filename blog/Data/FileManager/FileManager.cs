
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace blog.Data.FileManager
{
	public class FileManager: IFileManager
	{
		private string _imagePath;
		public FileManager(IConfiguration config)
		{
			_imagePath = config["Path:Images"];
		}

		public async Task<string> SaveImage(IFormFile image)
		{
			try
			{

			var save_path = Path.Combine(_imagePath);

			if(!Directory.Exists(save_path))
				{
				Directory.CreateDirectory(save_path);
			    }

			//Internet exp;orer error C:/User/Foo/image.jpg
			//var fileName = image.FileName;

			var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
			var fileName = $"img_{DateTime.Now.ToString("ddd-MM-yyyy-HH-mm-ss")}{mime}";

			using(var fileStream = new FileStream(Path.Combine(save_path,fileName), FileMode.Create))
			{
				await image.CopyToAsync(fileStream);

			}

			return fileName;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return "Error";

			}
		}
	}
}
