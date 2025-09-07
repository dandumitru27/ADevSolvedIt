using ADevSolvedIt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

using var serviceProvider = services.BuildServiceProvider();
using var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

var posts = db.Posts
    .Include(p => p.User)
    .OrderByDescending(p => p.LastEditDate)
    .ToList();

var projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
var readmePath = Path.Combine(projectRoot, "export", "README.md");

var readmeContent = $"# {posts.Count} posts\n\n";

foreach(var post in posts)
{
    var postFile = post.Slug + ".md";

    readmeContent += $"[{post.Title}](/export/posts/{postFile})  \n{post.LastEditDate.ToString("MMM d, yyyy")}; {post.Views} views\n\n";

    var postPath = Path.Combine(projectRoot, "export/posts", postFile);

    var postContent = $"# {post.Title}\n\n";

    postContent += $"Author: {post.User.Name}; Created: {post.CreationDate.ToString("MMMM d, yyyy")}; " +
        $"Last Edit: {post.LastEditDate.ToString("MMMM d, yyyy")}  \n";
    postContent += $"Tags: {post.Tags.Substring(1, post.Tags.Length - 2)}; Views: {post.Views.ToString("n0")}\n\n";

    postContent += "## Problem\n\n";
    postContent += post.ProblemMarkdown + "\n\n";

    postContent += "## Solution\n\n";
    postContent += post.SolutionMarkdown + "\n";

    File.WriteAllText(postPath, postContent);
}

File.WriteAllText(readmePath, readmeContent);

Console.WriteLine($"Exported {posts.Count} posts.");