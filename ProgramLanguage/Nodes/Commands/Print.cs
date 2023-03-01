using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgramLanguage.Nodes.Commands
{
    public class Print : Node
    {
        public Print() : base() { }
        public Print(Node node) : base(node.Raw, node.Line, node.Id, node.Interpretator) { }

        public List<Node> variables = new List<Node>();

        public override string ToString()
        {
            string str = "[" + GetType().Name + ":(";
            for (int i = 0; i < variables.Count; i++)
            {
                str += variables[i];
            }
            str += ")]";
            return str;
        }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out Print node))
            {
                int index = i + 1;
                if (IfNode.TryGetBracketSubInfo(ref index, ref nodes, out List<Node> variables))
                {
                    node.variables = variables;
                }
                IfNode.GetMethodVaraibles(ref node.variables);
                return true;
            }
            return false;
        }
        public override void Execute()
        {
            foreach(var node in variables)
            {
                node.Execute();
                object result = null;
                if (node.result is not null) result = node.result.GetResult();
                if(result is string)
                {
                    var result1 = (result as string);
                    var subResult = result1.Split("\\n");
                    for(int i = 0; i < subResult.Length; i++)
                    {
                        Console.Write(subResult[i]);
                        if (i != subResult.Length - 1) Console.WriteLine();
                    }
                }
                else if(result is not null) Console.Write(result);
            }
        }
    }
}
