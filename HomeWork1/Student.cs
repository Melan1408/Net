namespace HomeWork1
{
    public class Student : Person
    {

        protected double average;
        protected int numberOfGroup;

        public double Average { get; set; }
        public int NumberOfGroups { get; set; }

        public Student() {}

        public Student(double average, int numberOfGroup, string name, string surname, int age, string phone) : base(name, surname, age, phone)
        {
            Average = average;
            NumberOfGroups = numberOfGroup;
        }

        public new void Print()
        {
            base.Print();
            Console.WriteLine($"My Average is {Average} and number of groups is {NumberOfGroups}");
        }
    }
}
