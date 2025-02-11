using Bogus;
using Management.Service.Domain.Contracts.Dal.Entities;

namespace Management.Service.Domain.Fakers;

public sealed class FurnitureGoodEntityFaker : Faker<FurnitureGoodEntity>
{
    private static readonly string[] FurnitureTypes =
    [
        "Sofa", "Dining Table", "Bookshelf", "Bed Frame", "Coffee Table",
        "Recliner Chair", "Desk", "Wardrobe", "TV Stand", "Bar Stool",
        "Accent Chair", "Ottoman", "Console Table", "Bench", "Armchair",
        "Sectional Sofa", "Futon", "Chaise Lounge", "Rocking Chair", "Side Table",
        "Nightstand", "Dresser", "Chest of Drawers", "Mirror", "Vanity Table",
        "Bunk Bed", "Daybed", "Canopy Bed", "Sleigh Bed", "Platform Bed",
        "Folding Chair", "Folding Table", "Bean Bag Chair", "Loveseat", "Sofa Bed",
        "Cabinet", "Sideboard", "Buffet Table", "China Cabinet", "Display Cabinet",
        "Bar Cabinet", "Wine Rack", "Bookshelf Ladder", "Floating Shelf", "Corner Shelf",
        "Media Console", "Entertainment Center", "Record Cabinet", "Record Player Stand",
        "Writing Desk", "Computer Desk", "Executive Desk", "L-Shaped Desk", "Standing Desk",
        "Filing Cabinet", "Office Chair", "Task Chair", "Ergonomic Chair", "Conference Table",
        "Folding Screen", "Room Divider", "Hall Tree", "Coat Rack", "Shoe Rack",
        "Umbrella Stand", "Plant Stand", "Fireplace Mantel", "Fireplace Screen", "Fireplace Tool Set",
        "Outdoor Sofa", "Outdoor Dining Table", "Outdoor Lounge Chair", "Outdoor Bench", "Outdoor Daybed",
        "Patio Umbrella", "Outdoor Coffee Table", "Adirondack Chair", "Hammock", "Porch Swing",
        "Picnic Table", "Garden Bench", "Outdoor Bar Stool", "Outdoor Sectional", "Outdoor Chaise Lounge",
        "Outdoor Storage Box", "Outdoor Side Table", "Outdoor Rocking Chair", "Outdoor Bar Cart",
        "Outdoor Fire Pit", "Outdoor Dining Chair", "Outdoor Cushions", "Outdoor Rug", "Outdoor Planters",
        "Outdoor Lighting", "Outdoor Curtains", "Outdoor Privacy Screen", "Outdoor Pergola", "Outdoor Gazebo"
    ];
    
    public FurnitureGoodEntityFaker()
    {
        RuleFor(e => e.Id, faker => faker.Random.Long(0L));
        RuleFor(e => e.Price, faker => faker.Random.Decimal(0M, 1_000_000M));
        RuleFor(e => e.ReleaseDate, faker => faker.Date.BetweenDateOnly(new DateOnly(1970, 01, 01), DateOnly.FromDateTime(DateTime.UtcNow)));
        RuleFor(e => e.Name, faker => $"{faker.Commerce.ProductAdjective()} {faker.Random.ArrayElement(FurnitureTypes)}");
    }
}