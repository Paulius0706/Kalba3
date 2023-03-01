using System.Text.RegularExpressions;

namespace ProgramLanguage
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // .(?<=\')[^\']*(?=\').|[a-zA-z]+[a-zA-z0-9]*|\+\+|\-\-|==|<|>|<=|>=|!=|[\(\)\{\}\[\];,\.\n=\-\+\*\/\^<>&|]|[0-9]+\.[0-9]+|[0-9]+
            string regexString = ".(?<=\\\")[^\\\"]*(?=\\\").|[a-zA-z]+[a-zA-z0-9]*|\\+\\+|\\-\\-|==|<=|>=|!=|[\\(\\)\\{\\}\\[\\];,\\.\\n=\\-\\+\\*\\/\\^<>&|]|[0-9]+\\.[0-9]+|[0-9]+";
            Regex regex = new Regex(regexString);
            string text = File.ReadAllText("data.txt");
            MatchCollection matchCollection = regex.Matches(text);
            List<Match> matches = matchCollection.ToList();
            Interpretator interpretator = new Interpretator(matches);
            interpretator.Compress();
            interpretator.WriteAllNodes();
            interpretator.Execute(interpretator.nodes);
            //WriteTokens(matches);

        }
        public static void WriteTokens(List<Match> matches)
        {
            foreach (var match in matches)
            {
                if (String.IsNullOrEmpty(match.Value)) continue;
                if (match.Value == "\n") { Console.Write("\n"); continue; }
                Console.Write("[" + match.Value + "]");
            }
        }
    }
}