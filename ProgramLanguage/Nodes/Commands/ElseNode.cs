using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Commands
{
    internal class ElseNode : Command
    {
        public ElseNode(Node node) : base(node) { }

        public List<Node> innnerNodes = new List<Node>();

        public override string ToString()
        {
            string str = "[" + GetType().Name + ":";
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
            if (nodes[i].TryGetNode(out ElseNode node))
            {
                int index = i + 1;
                if (IfNode.TryGetCurlyBracketSubInfo(ref index, ref nodes, out List<Node> innerNodes))
                {
                    node.innnerNodes = innerNodes;
                    Interpretator innerNodesInterpretator = new Interpretator();
                    innerNodesInterpretator.CompressRec(ref node.innnerNodes);

                }
                else if(IfNode.TryGetTillSemiColumOrCurlyBracket(ref index, ref nodes, out List<Node> oneLiner))
                {
                    node.innnerNodes = oneLiner;
                    Interpretator innerNodesInterpretator = new Interpretator();
                    innerNodesInterpretator.CompressRec(ref node.innnerNodes);
                }
                return true;
            }
            return false;
        }
        public override void Execute()
        {

            Interpretator interpretator = new Interpretator(Interpretator);
            List<Node> nodes = new List<Node>();
            foreach (var node in innnerNodes)
            {
                node.Interpretator = interpretator;
                node.AssignNewInterpretator(interpretator);
                nodes.Add(node);
            }
            interpretator.Execute(nodes);
        }
        public override void AssignNewInterpretator(Interpretator interpretator)
        {
            foreach(Node node in innnerNodes)
            {
                node.Interpretator = interpretator;
                node.AssignNewInterpretator(interpretator);
            }
        }
    }
}
