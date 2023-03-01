using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Commands
{
    public class WhileNode : Command
    {
        public List<Node> innnerNodes = new List<Node>();
        public List<Node> variables = new List<Node>();
        public WhileNode(Node node) : base(node) { }


        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out WhileNode node))
            {
                int index = i + 1;
                if (WhileNode.TryGetBracketSubInfo(ref index, ref nodes, out List<Node> variables))
                {
                    node.variables = variables;
                    node.AritmeticCompressRec(ref node.variables);

                }
                if (WhileNode.TryGetCurlyBracketSubInfo(ref index, ref nodes, out List<Node> innerNodes))
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
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            bool isTrue;
            Interpretator interpretator = new Interpretator(Interpretator);
            Interpretator variableInterpretator = new Interpretator(interpretator);

            // is true
            List<Node> variables = new List<Node>();
            foreach (var node in this.variables)
            {
                node.Interpretator = variableInterpretator;
                node.AssignNewInterpretator(variableInterpretator);
                variables.Add(node);
            }
            variables[0].Execute();
            isTrue = (bool)variables[0].result.GetResult();
            while (isTrue)
            {
                List<Node> nodes = new List<Node>();
                foreach (var node in innnerNodes)
                {
                    node.Interpretator = interpretator;
                    node.AssignNewInterpretator(interpretator);
                    nodes.Add(node);
                }
                interpretator.Execute(nodes);

                // is true
                variables = new List<Node>();
                foreach (var node in this.variables)
                {
                    node.Interpretator = variableInterpretator;
                    node.AssignNewInterpretator(variableInterpretator);
                    variables.Add(node);
                }
                variables[0].Execute();
                isTrue = (bool)variables[0].result.GetResult();
            }
        }
    }
}
