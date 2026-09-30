namespace AutomotiveIntelligence.Domain.Entities;

public class Model
{
    //Unique ID for Corolla, Civic, Yaris, etc.
    public int ModelId { get; set; }
    //Links the model to its manufacturer.
    public int MakeId { get; set; }
    //Corolla, Civic, Yaris, etc.
    public string Name { get; set; } = string.Empty;
    //Sedan, SUV, Hatchback, etc.
    public string? BodyType { get; set; }
    //Allows us to deactivate a model without deleting historical data.
    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}