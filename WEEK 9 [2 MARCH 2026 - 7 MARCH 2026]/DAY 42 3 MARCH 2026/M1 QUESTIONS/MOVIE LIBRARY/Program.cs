using System;
using System.Collections.Generic;
using System.Linq;

public interface IFilm
{
    string Title { get; set; }
    string Director { get; set; }
    int Year { get; set; }
}

public interface IFilmLibrary
{
    void AddFilm(IFilm film);
    void RemoveFilm(string title);
    List<IFilm> GetFilms();
    List<IFilm> SearchFilms(string query);
    int GetTotalFilmCount();
}

public class Film : IFilm
{
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
}

public class FilmLibrary : IFilmLibrary
{
    private List<IFilm> films = new List<IFilm>();

    public void AddFilm(IFilm film)
    {
        films.Add(film);
    }

    public void RemoveFilm(string title)
    {
        var film = films.FirstOrDefault(f => f.Title == title);
        if (film != null)
            films.Remove(film);
    }

    public List<IFilm> GetFilms()
    {
        return films;
    }

    public List<IFilm> SearchFilms(string query)
    {
        return films.Where(f =>
            f.Title.ToLower().Contains(query.ToLower()) ||
            f.Director.ToLower().Contains(query.ToLower())
        ).ToList();
    }

    public int GetTotalFilmCount()
    {
        return films.Count;
    }
}

class Program
{
    static void Main()
    {
        FilmLibrary library = new FilmLibrary();

        library.AddFilm(new Film { Title = "Inception", Director = "Christopher Nolan", Year = 2010 });
        library.AddFilm(new Film { Title = "Interstellar", Director = "Christopher Nolan", Year = 2014 });
        library.AddFilm(new Film { Title = "Titanic", Director = "James Cameron", Year = 1997 });

        Console.WriteLine("All Movies:");
        foreach (var film in library.GetFilms())
        {
            Console.WriteLine($"{film.Title} - {film.Director} ({film.Year})");
        }

        Console.WriteLine("\nSearch for 'Nolan':");
        var results = library.SearchFilms("Nolan");
        foreach (var film in results)
        {
            Console.WriteLine($"{film.Title} - {film.Director}");
        }

        Console.WriteLine("\nTotal Films: " + library.GetTotalFilmCount());

        library.RemoveFilm("Titanic");

        Console.WriteLine("\nAfter Removing Titanic:");
        foreach (var film in library.GetFilms())
        {
            Console.WriteLine($"{film.Title} - {film.Director}");
        }

        Console.ReadLine();
    }
}