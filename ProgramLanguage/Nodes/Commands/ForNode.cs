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
                if (i + 1 < nodes.Count && nodes[i + 1].Raw == "(")
                {
                    nodes.RemoveAt(i + 1);
                    int leftBrachetCount = 1;
                    int rightBrachetCount = 0;
                    List<Node> variables = new List<Node>();
                    while (nodes.Count > i + 1 && leftBrachetCount > rightBrachetCount)
                    {
                        if (nodes[i + 1].Raw == ")")
                        {
                            rightBrachetCount++;
                            if (leftBrachetCount > rightBrachetCount)
                            {
                                variables.Add(nodes[i + 1]);
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
                            variables.Add(nodes[i + 1]);
                            nodes.RemoveAt(i + 1);
                        }
                    }
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
                    //variables.RemoveAt(0);


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
