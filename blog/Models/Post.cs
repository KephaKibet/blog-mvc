namespace blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
   
    
    }
}
