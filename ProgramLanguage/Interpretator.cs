using ProgramLanguage.Nodes;
using ProgramLanguage.Nodes.Commands;
using ProgramLanguage.Nodes.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramLanguage
{
    public class Interpretator
    {
        public Interpretator parent;
        private static int interpetatorCounter = 0;
        public List<Node> nodes;
        public static Dictionary<string,Primitive> primitives = new Dictionary<string, Primitive>();
        public static Dictionary<string, AddMethodNode> methhods = new Dictionary<string, AddMethodNode>();
        public readonly int Id;
        public Interpretator() { }
        public Interpretator(List<Match> matches)
        {
            Id = interpetatorCounter++;
            nodes = new List<Node>();
            primitives = new Dictionary<string, Primitive>();
            int line = 1;
            foreach (Match match in matches)
            {
                if (String.IsNullOrEmpty(match.Value)) continue;
                if (match.Value.Contains('\n')) { line++; continue; }
                Node node = new Node(match.Value, line, interpetatorCounter++,this);
                nodes.Add(node);
            }

            Regex floatRegex = new Regex("[0-9]+\\.[0-9]+");
            Regex intRegex = new Regex("[0-9]+");
            Regex textRegex = new Regex("[a-zA-z]+[a-zA-z0-9]*");
            // rewrite nodes
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Raw == "-") { nodes[i] = new Sub(nodes[i]); continue; }
                if (nodes[i].Raw == "+") { nodes[i] = new Add(nodes[i]); continue; }
                if (nodes[i].Raw == "*") { nodes[i] = new Mul(nodes[i]); continue; }
                if (nodes[i].Raw == "/") { nodes[i] = new Div(nodes[i]); continue; }

                if (nodes[i].Raw == "=") { nodes[i] = new Assign(nodes[i]); continue; }
                
                if (nodes[i].Raw == ">") { nodes[i] = new More(nodes[i]); continue; }
                if (nodes[i].Raw == "<") { nodes[i] = new Less(nodes[i]); continue; }
                if (nodes[i].Raw == ">=") { nodes[i] = new MoreEqual(nodes[i]); continue; }
                if (nodes[i].Raw == "<=") { nodes[i] = new LessEqual(nodes[i]); continue; }
                if (nodes[i].Raw == "!=") { nodes[i] = new NotEqual(nodes[i]); continue; }
                if (nodes[i].Raw == "&") { nodes[i] = new And(nodes[i]); continue; }
                if (nodes[i].Raw == "|") { nodes[i] = new Or(nodes[i]); continue; }

                if (nodes[i].Raw == "print") { nodes[i] = new Print(nodes[i]); continue; }

                if (nodes[i].Raw == "null") { nodes[i] = new PNull(nodes[i]); continue; }
                if (nodes[i].Raw == "int") { nodes[i] = new PDInt(nodes[i]); continue; }
                if (nodes[i].Raw == "float") { nodes[i] = new PDFloat(nodes[i]); continue; }
                if (nodes[i].Raw == "bool") { nodes[i] = new PDBool(nodes[i]); continue; }
                if (nodes[i].Raw == "string") { nodes[i] = new PDString(nodes[i]); continue; }
                if (nodes[i].Raw == "char") { nodes[i] = new PDString(nodes[i]); continue; }
                if (nodes[i].Raw == "arr") { nodes[i] = new PDArray(nodes[i]); continue; }
                
                if (nodes[i].Raw == "def") { nodes[i] = new AddMethodNode(nodes[i]); continue; }
                if (nodes[i].Raw == "for") { nodes[i] = new ForNode(nodes[i]); continue; }
                if (nodes[i].Raw == "if") { nodes[i] = new IfNode(nodes[i]); continue; }
                if (nodes[i].Raw == "else") { nodes[i] = new ElseNode(nodes[i]); continue; }
                if (nodes[i].Raw == "while") { nodes[i] = new WhileNode(nodes[i]); continue; }

                if (IsMatch(floatRegex,nodes[i].Raw)) { nodes[i] = new PFloat(nodes[i]); continue; }
                if (IsMatch(intRegex,nodes[i].Raw)) { nodes[i] = new PInt(nodes[i]); continue; }
                if (IsMatch(textRegex, nodes[i].Raw))
                {
                    if (BracketsAfter(i) && !DefBehind(i))
                    {
                        nodes[i] = new MethodNode(nodes[i]); continue;
                    }
                    else if (!DefBehind(i))
                    {
                        nodes[i] = new PVariable(nodes[i]); continue;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        public void Compress()
        {
            CompressRec(ref nodes);
        }
        public void CompressRec(ref List<Node> nodes)
        {
            for(int i = 0; i < nodes.Count; i++)
            {
                if (Assign.Compress(ref i, ref nodes)) continue;
                if (AddMethodNode.Compress(ref i, ref nodes)) continue;
                if (ForNode.Compress(ref i, ref nodes)) continue;
                if (IfNode.Compress(ref i, ref nodes)) continue;
                if (MethodNode.Compress(ref i, ref nodes)) continue;
                if (Print.Compress(ref i, ref nodes)) continue;
                if (ElseNode.Compress(ref i, ref nodes)) continue;
            }
        }
        public bool DefBehind(int i)
        {
            if (i > 0) return false;
            return nodes[i - 1].GetType().Name == nameof(AddMethodNode);
        }
        public bool BracketsAfter(int i)
        {
            if (nodes.Count <= i+1) return false;
            return nodes[i+1].Raw == "(";
        }
        public bool IsMatch(Regex regex, string value)
        {
            return regex.IsMatch(value) && regex.Match(value).Value.Length == value.Length;
        }
        public void WriteAllNodes() 
        {
            Console.WriteLine(new String('=',50));
            Console.Write(nodes[0]);
            for (int i = 1; i < nodes.Count; i++)
            {
                if(nodes[i].Line - nodes[i - 1].Line >0) Console.Write("\n");
                //for (int j = 0; j < nodes[i].Line - nodes[i - 1].Line; j++) { Console.Write("\n"); }
                Console.Write(nodes[i]);
            }
            Console.WriteLine();
            Console.WriteLine(new String('=', 50));
        }
    }
}
