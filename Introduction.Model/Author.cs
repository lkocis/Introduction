namespace Introduction.Model

{
    public class Author
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }

        public Author(Guid id, string firstName, string lastName, DateTime dob)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            DOB = dob;
        }

        public Author()
        { }
    }
}