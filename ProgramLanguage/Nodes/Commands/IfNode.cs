using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Commands
{
    internal class IfNode : Command
    {
        public IfNode(Node node) : base(node) { }

        public List<Node> innnerNodes = new List<Node>();
        public List<Node> variables = new List<Node>();

        public override string ToString()
        {
            string str = "[" + GetType().Name + ":(";
            for (int i = 0; i < variables.Count; i++)
            {
                str += variables[i];
            }
            str += "):";
            str += "{\n";
            for (int i = 0; i < innnerNodes.Count; i++)
            {
                if (i > 0 && innnerNodes[i - 1].Line < innnerNodes[i].Line) str += "\n";
                str += innnerNodes[i];
            }
            str += "\n}]";
            return str;
        }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out IfNode node))
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
                    node.AritmeticCompressRec(ref node.variables);
                }
                if (i + 1 < nodes.Count && nodes[i + 1].Raw == "{")
                {
                    nodes.RemoveAt(i + 1);
                    int leftBrachetCount = 1;
                    int rightBrachetCount = 0;
                    while (nodes.Count > i + 1 && leftBrachetCount > rightBrachetCount)
                    {
                        if (nodes[i + 1].Raw == "}")
                        {
                            rightBrachetCount++;
                            if (leftBrachetCount > rightBrachetCount)
                            {
                                node.innnerNodes.Add(nodes[i + 1]);
                                nodes.RemoveAt(i + 1);
                            }
                            else
                            {
                                nodes.RemoveAt(i + 1);
                            }
                        }
                        else
                        {
                            if (nodes[i + 1].Raw == "{") leftBrachetCount++;
                            node.innnerNodes.Add(nodes[i + 1]);
                            nodes.RemoveAt(i + 1);
                        }
                    }
                }
                Interpretator interpretator = new Interpretator();
                interpretator.CompressRec(ref node.innnerNodes);
                return true;
            }
            return false;
        }
    }
}
