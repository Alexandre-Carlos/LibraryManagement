﻿using LibraryManagement.Api.Entities;

namespace LibraryManagement.Api.Dtos.Books
{
    public class BookUpdateRequestDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int YearPublished { get; set; }

        public Book ToEntity()
            => new (Title,Author,Isbn,YearPublished);
    }
}
