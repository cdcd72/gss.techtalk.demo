namespace InitAccessor.Demo.Model
{
    public class Person
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public Person(string firstName, string lastName) 
            => (FirstName, LastName) = (firstName, lastName);
    }
}
