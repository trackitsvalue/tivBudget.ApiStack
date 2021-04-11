using System.Collections.Generic;

namespace tivBudget.Api.Models
{
  public class NewsTimelineBox
  {
    /// <summary>The Label for this section of the news item.</summary>
    public string SectionLabel { get; set; }

    /// <summary>This section of the news item.</summary>
    public List<NewsTimeline> SectionData { get; set; } = new List<NewsTimeline>();
  }

  public class NewsTimeline
  {
    /// <summary> A date representation for when the news item occurred.</summary>
    public string Date { get; set; }

    /// <summary>The content of the news item.</summary>
    public string Content { get; set; }

    /// <summary>The title of the news item.</summary>
    public string Title { get; set; }

    /// <summary>The icon representing the news item.</summary>
    public string Icon { get; set; }

    /// <summary>The Icon background color of the news item.</summary>
    public string IconBg { get; set; }

    /// <summary>The Icon foreground color of the news item.</summary>
    public string IconColor { get; set; }

  }
}