﻿using System.Data;

namespace LibraryManagement.Core.Entities
{
    public class Book : BaseEntity
    {
        public Book() { }      
        
        public Book(string title, string author, string isbn, int yearPublished, int quantity) : base()
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            YearPublished = yearPublished;
            Quantity = quantity;
        }
        public Book(string title, string author, string isbn, int yearPublished) : base()
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            YearPublished = yearPublished;
            Loans = [];
        }

        public string Title { get; private set; }
        public int YearPublished { get; private set; }
        public string Isbn { get; private set; }
        public string Author { get; private set; }
        public int Quantity { get; private set; }

        public List<Loan> Loans {  get; private set; }

        public void SetIncrementQuantity() => Quantity ++;

        public void SetDecrementQuantity()
        {
            if (Quantity-- >= 0)
                Quantity--;
            else
                throw new InvalidOperationException("Quantidade indisponível!");
        }

        public void SetAddQuantity(int quantity) => Quantity += quantity;

        public void SetTitle(string title) => Title = title;

        public void SetAuthor(string author) => Author = author;

        public void SetIsbn(string isbn) => Isbn = isbn;

        public void SetYearPublished(int yearPublished) => YearPublished = yearPublished;


        public void Update(string title, string author, string isbn, int yearPublished)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            YearPublished = yearPublished;
        }

    }
}
