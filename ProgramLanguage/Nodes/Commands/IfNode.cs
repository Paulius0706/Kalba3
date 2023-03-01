using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
        public ElseNode elseNode;

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
            str += "\n}";
            if(elseNode is not null)
            {
                str += ":";
                str += elseNode;
                str += "";
            }
            str += "]";
            return str;
        }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out IfNode node))
            {
                int index = i + 1;
                if (IfNode.TryGetBracketSubInfo(ref index, ref nodes, out List<Node> variables))
                {
                    node.variables = variables;
                    node.AritmeticCompressRec(ref node.variables);

                }
                if (IfNode.TryGetCurlyBracketSubInfo(ref index, ref nodes, out List<Node> innerNodes))
                {
                    node.innnerNodes = innerNodes;
                    Interpretator innerNodesInterpretator = new Interpretator();
                    innerNodesInterpretator.CompressRec(ref node.innnerNodes);

                }
                else if (IfNode.TryGetTillSemiColumOrCurlyBracket(ref index, ref nodes, out List<Node> oneLiner))
                {
                    node.innnerNodes = oneLiner;
                    Interpretator innerNodesInterpretator = new Interpretator();
                    innerNodesInterpretator.CompressRec(ref node.innnerNodes);
                }
                if (nodes.Count > index && ElseNode.Compress(ref index, ref nodes) && nodes[index].TryGetNode(out ElseNode elseNode)) 
                {
                    node.elseNode = elseNode;
                    nodes.RemoveAt(index);
                }
                return true;
            }
            return false;
        }

        public override void Execute()
        {            
            Interpretator interpretator = new Interpretator(Interpretator);
            Interpretator variableInterpretator = new Interpretator(interpretator);

            List<Node> variables = new List<Node>();
            foreach (var node in this.variables)
            {
                node.Interpretator = variableInterpretator;
                node.AssignNewInterpretator(variableInterpretator);
                variables.Add(node);
            }
            variables[0].Execute();
            bool isTrue = (bool)variables[0].result.GetResult();

            if (isTrue)
            {
                List<Node> nodes = new List<Node>();
                foreach (var node in innnerNodes)
                {
                    node.Interpretator = interpretator;
                    node.AssignNewInterpretator(interpretator);
                    nodes.Add(node);
                }
                interpretator.Execute(nodes);
            }
            else if(elseNode is not null)
            {
                elseNode.Interpretator = interpretator;
                elseNode.AssignNewInterpretator(interpretator);
                elseNode.Execute();
            }
        }
    }
}
