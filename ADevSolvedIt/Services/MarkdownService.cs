using Markdig;

namespace ADevSolvedIt.Services;

public class MarkdownService
{
    public static string ToSafeHtml(string inputMarkdown)
    {
        var pipeline = new MarkdownPipelineBuilder().DisableHtml().Build();
        return Markdown.ToHtml(inputMarkdown, pipeline);
    }
}
