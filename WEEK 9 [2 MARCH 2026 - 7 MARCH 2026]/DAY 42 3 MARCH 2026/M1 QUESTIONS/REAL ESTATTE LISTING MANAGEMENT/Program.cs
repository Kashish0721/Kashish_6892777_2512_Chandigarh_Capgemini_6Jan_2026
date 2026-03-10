using System;
using System.Collections.Generic;
using System.Linq;

public class RealEstateListing
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Location { get; set; }
}

public class RealEstateApp
{
    private List<RealEstateListing> listings = new List<RealEstateListing>();

    public void AddListing(RealEstateListing listing)
    {
        listings.Add(listing);
    }

    public void RemoveListing(int id)
    {
        var listing = listings.FirstOrDefault(x => x.ID == id);
        if (listing != null)
            listings.Remove(listing);
    }

    public void UpdateListing(RealEstateListing updatedListing)
    {b
        var listing = listings.FirstOrDefault(x => x.ID == updatedListing.ID);

        if (listing != null)
        {
            listing.Title = updatedListing.Title;
            listing.Description = updatedListing.Description;
            listing.Price = updatedListing.Price;
            listing.Location = updatedListing.Location;
        }
    }

    public List<RealEstateListing> GetListings()
    {
        return listings;
    }

    public List<RealEstateListing> GetListingsByLocation(string location)
    {
        return listings.Where(x => x.Location.Equals(location, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<RealEstateListing> GetListingsByPriceRange(int minPrice, int maxPrice)
    {
        return listings.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
    }
}

class Program
{
    static void Main()
    {
        RealEstateApp app = new RealEstateApp();

        app.AddListing(new RealEstateListing
        {
            ID = 1,
            Title = "Luxury Villa",
            Description = "5 BHK Sea View",
            Price = 9000000,
            Location = "Goa"
        });

        app.AddListing(new RealEstateListing
        {
            ID = 2,
            Title = "City Apartment",
            Description = "3 BHK near Metro",
            Price = 4500000,
            Location = "Delhi"
        });

        app.AddListing(new RealEstateListing
        {
            ID = 3,
            Title = "Farm House",
            Description = "Nature View",
            Price = 6000000,
            Location = "Goa"
        });

        Console.WriteLine("All Listings:");
        foreach (var l in app.GetListings())
            Console.WriteLine($"{l.Title} - {l.Location} - {l.Price}");

        Console.WriteLine("\nListings in Goa:");
        foreach (var l in app.GetListingsByLocation("Goa"))
            Console.WriteLine($"{l.Title} - {l.Price}");

        Console.WriteLine("\nListings between 4M and 7M:");
        foreach (var l in app.GetListingsByPriceRange(4000000, 7000000))
            Console.WriteLine($"{l.Title} - {l.Price}");

        Console.ReadLine();
    }
}