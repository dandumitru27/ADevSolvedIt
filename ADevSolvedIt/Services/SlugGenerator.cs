using System.Text;
using System.Text.RegularExpressions;

namespace ADevSolvedIt.Services
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(string title)
        {
            string output = Regex.Replace(title, "#", "-sharp");

            output = RemoveAccents(output).ToLower();

            output = Regex.Replace(output, @"[^a-z0-9\s-]", ""); // invalid chars
            output = Regex.Replace(output, @"\s+", " ").Trim(); // convert multiple spaces into one space
            output = output.Substring(0, output.Length <= 100 ? output.Length : 100).Trim(); // cut and trim it
            output = Regex.Replace(output, @"\s", "-"); // hyphens

            return output;
        }

        private static string RemoveAccents(string input)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(input);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
