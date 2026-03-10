using System.Text.Json;

using System;
using System.Text.Json;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        // Step 1: Create object
        Student s = new Student { Id = 1, Name = "John Doe" };

        // Step 2: Serialize to JSON string
        string jsonString = JsonSerializer.Serialize(s);
        Console.WriteLine("Serialized JSON: " + jsonString);

        // Step 3: Deserialize back to object
        Student deserialized = JsonSerializer.Deserialize<Student>(jsonString);

        // Step 4: Use the deserialized object
        Console.WriteLine($"Id: {deserialized.Id}, Name: {deserialized.Name}");
    }
}
