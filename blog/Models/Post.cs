﻿namespace blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = "";
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
   
    
    }
}
