namespace Introduction.Model

{
    public class Author
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public Book? Book { get; set; }
        public Guid? BookId { get; set; }

        public Author(Guid id, string firstName, string lastName, DateTime dob, Book book, Guid bookId)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            DOB = dob;
            BookId = Book.Id;
            Book = book;
        }

        public Author()
        { }
    }
}