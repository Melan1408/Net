namespace HomeWork1
{
    public class Person
    {
        protected string name;
        protected string surname;
        protected int age;
        protected string phone;

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }

        public Person() { }
        public Person(string name, string surname, int age, string phone)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Phone = phone;
        }

        public void Print()
        {
            Console.WriteLine($"Hello! Im {Name} {Surname},my age is {Age} and phone is {Phone}");
        }
    }
}
