namespace InitAccessor.Demo.Model
{
    public class Teacher : Person
    {
        public string Subject { get; init; }

        public Teacher(string firstName, string lastName, string subject) : base(firstName, lastName) 
            => Subject = subject;
    }
}
