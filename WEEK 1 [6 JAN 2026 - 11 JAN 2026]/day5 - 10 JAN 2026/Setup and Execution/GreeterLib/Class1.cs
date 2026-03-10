namespace GreeterLib;

public interface IGreeter
{
    string Greet(string name);
}

public class Greeter : IGreeter
{
    public string Greet(string name)
    {
        return $"Hello, {name} from Library!";
    }
}
