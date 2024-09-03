namespace blog.Models
{
    public class Post
    {
        public string Tittle { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
   
    
    }
}
