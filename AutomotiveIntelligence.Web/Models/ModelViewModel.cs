namespace AutomotiveIntelligence.Web.Models;

public class ModelViewModel
{
    public int ModelId { get; set; }

    public int MakeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? BodyType { get; set; }
}