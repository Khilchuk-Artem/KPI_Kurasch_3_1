﻿namespace BookLibrary.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        //public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}
