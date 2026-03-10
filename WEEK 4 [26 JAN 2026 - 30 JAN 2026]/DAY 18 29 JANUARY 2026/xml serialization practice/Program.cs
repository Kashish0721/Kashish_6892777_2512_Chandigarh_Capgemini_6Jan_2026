using System;
using System.IO;
using System.Xml.Serialization;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        Student s = new Student { Id = 2, Name = "Alice" };

        // Step 1: Serialize to XML
        XmlSerializer xs = new XmlSerializer(typeof(Student));
        using (TextWriter tw = new StreamWriter("student.xml"))
        {
            xs.Serialize(tw, s);
        }

        // Step 2: Deserialize from XML
        using (TextReader tr = new StreamReader("student.xml"))
        {
            Student deserialized = (Student)xs.Deserialize(tr);
            Console.WriteLine($"Id: {deserialized.Id}, Name: {deserialized.Name}");
        }
    }
}
