using System.Xml.Serialization;

namespace HomeWork1
{
    public class AcademyGroup
    {
        private List<Student> students = new List<Student>();
        private int count = 0;
        private static string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../academyGroup.txt"));
        public int Count { get; }

        public void Add(Student student)
        {
            students.Add(student);
            count ++;
        }

        public void Remove(string surname)
        {
            var studentForRemove = students.FirstOrDefault(student => student.Surname == surname);

            if (studentForRemove != null)
            {
                students.Remove(studentForRemove);
                count --;
            }
        }

        public void Edite(string surname, Student student)
        {
            var studentIndexForEdit = students.FindIndex(student => student.Surname == surname);
            if (studentIndexForEdit != -1)
            {
                students[studentIndexForEdit] = student;
            }
        }

        public void Search(string surname)
        {
            var student = students.FirstOrDefault(student => student.Surname == surname);
            if (student != null)
            {
                student.Print();
            }
            else
            {
                Console.WriteLine($"There is no student with this {surname} surname!");
            }
        }


        public void Print()
        {
           students.ForEach(student => student.Print());
        }

        public void Save()
        {           
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                writer = new StreamWriter(path);
                serializer.Serialize(writer, students);
            }
            catch(Exception ex)
            { 
                Console.WriteLine(ex.ToString()); 
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

        }

        public void Read()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                reader = new StreamReader(path);
                students = (List<Student>)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString()); 
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }
}
