using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgramLanguage.Nodes.Commands
{
    public class ForNode : Command
    {
        public ForNode(Node node) : base(node) { }

        public List<Node> innnerNodes = new List<Node>();

        public List<Node> variables = new List<Node>();
        public List<Node> start = new List<Node>();
        public Node continueExpression;
        public List<Node> after = new List<Node>();

        public override string ToString()
        {
            string str = "[" + GetType().Name + ":(";
            for (int i = 0; i < start.Count; i++)
            {
                str += start[i];
            }
            str += "):("+continueExpression+"):";
            str += "(";
            for (int i = 0; i < after.Count; i++)
            {
                str += after[i];
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
            if (nodes[i].TryGetNode(out ForNode node))
            {
                int index = i + 1;
                if (IfNode.TryGetBracketSubInfo(ref index, ref nodes, out List<Node> variables))
                {
                    node.variables = variables;

                    // start
                    while (variables.Count > 0 && variables[0].Raw != ";")
                    {
                        node.start.Add(variables[0]);
                        variables.RemoveAt(0);
                    }
                    Interpretator startOnterpretator = new Interpretator();
                    startOnterpretator.CompressRec(ref node.start);
                    variables.RemoveAt(0);

                    // continueExpression
                    List<Node> continueExpression = new List<Node>();
                    while (variables.Count > 0 && variables[0].Raw != ";")
                    {
                        continueExpression.Add(variables[0]);
                        variables.RemoveAt(0);
                    }
                    node.AritmeticCompressRec(ref continueExpression);
                    node.continueExpression = continueExpression[0];
                    variables.RemoveAt(0);

                    // afterExpression
                    while (variables.Count > 0 && variables[0].Raw != ";")
                    {
                        node.after.Add(variables[0]);
                        variables.RemoveAt(0);
                    }
                    Interpretator afterInterpretator = new Interpretator();
                    afterInterpretator.CompressRec(ref node.after);
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
                return true;
            }
            return false;
        }
        public override void Execute()
        {
            
        }
    }
}
