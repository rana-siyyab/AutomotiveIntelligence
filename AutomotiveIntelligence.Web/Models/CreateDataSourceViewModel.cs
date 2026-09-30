using System.ComponentModel.DataAnnotations;

namespace AutomotiveIntelligence.Web.Models;

public class CreateDataSourceViewModel
{
    [Required]
    [Display(Name = "Source Name")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Source Type")]
    [StringLength(50)]
    public string SourceType { get; set; } = string.Empty;

    [Display(Name = "Base URL")]
    [Url]
    [StringLength(500)]
    public string? BaseUrl { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}