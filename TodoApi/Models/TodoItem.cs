using System
using System.ComponentModels.DataAnnotations


namespace TodoApi.Models
{
  public class TodoItem
  {
    public long Id { get; set; }

    [Required(ErrorMessage = "Name is Required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityLevel Priority { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime DueDate { get; set; } = DateTime.MaxValue;
    public DateTime CompletedAt { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
  }

  public enum PriorityLevel
  {
    Low,
    Medium,
    High
  }
}
