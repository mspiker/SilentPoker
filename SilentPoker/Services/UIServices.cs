using System.Text.RegularExpressions;

namespace SilentPoker.Services
{
    public static class UIServices
    {
        public static string TrimLongWords(string original, int maxCount)
        {
            return string.Join(" ",
            original.Split(' ')
            .Select(x =>
            {
                var r = Regex.Replace(x, @"\W", "");
                return r.Substring(0, r.Length > maxCount ? maxCount : r.Length) + Regex.Replace(x, @"\w", "");
            }));
        }
    }
}
