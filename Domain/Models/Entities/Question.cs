using Domain.Enums;
using Domain.Models.Common;

namespace Domain.Models.Entities;

public class Question:BaseAuditableEntity
{
    public string? QuestionText { get; set; }
    public string? OptionA { get; set; }
    public string? OptionB { get; set; }
    public string? OptionC { get; set; }
    public string? OptionD { get; set; }
    public bool isTrueA { get; set; } = false;
    public bool isTrueB { get; set; } = false;
    public bool isTrueC { get; set; } = false;
    public bool isTrueD { get; set; } = false;
    public Difficulty Difficulty { get; set; } = Difficulty.Easy;

    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    public Guid InnerCategoryId { get; set; }
    public virtual InnerCategory? InnerCategory { get; set; }
}
