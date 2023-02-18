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
                if (i + 1 < nodes.Count && nodes[i + 1].Raw == "(")
                {
                    nodes.RemoveAt(i + 1);
                    int leftBrachetCount = 1;
                    int rightBrachetCount = 0;
                    while (nodes.Count > i + 1 && leftBrachetCount > rightBrachetCount)
                    {
                        if (nodes[i + 1].Raw == ")")
                        {
                            rightBrachetCount++;
                            if (leftBrachetCount > rightBrachetCount)
                            {
                                node.variables.Add(nodes[i + 1]);
                                nodes.RemoveAt(i + 1);
                            }
                            else
                            {
                                nodes.RemoveAt(i + 1);
                            }
                        }
                        else
                        {
                            if (nodes[i + 1].Raw == "(") leftBrachetCount++;
                            node.variables.Add(nodes[i + 1]);
                            nodes.RemoveAt(i + 1);
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
