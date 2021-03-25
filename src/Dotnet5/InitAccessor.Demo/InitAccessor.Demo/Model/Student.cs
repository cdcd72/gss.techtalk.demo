namespace InitAccessor.Demo.Model
{
    public class Student : Person
    {
        public int Level { get; init; }

        public Student(string firstName, string lastName, int level) : base(firstName, lastName)
            => Level = level;
    }
}
