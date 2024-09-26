namespace Introduction.Model

{
    public class Author
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? Image { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}