using ProgramLanguage.Nodes.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Commands
{
    public class AddMethodNode : Command
    {
        public AddMethodNode(Node node) : base(node) { }
        public string Name { get; private set; }

        public List<Node> innnerNodes = new List<Node>();
        public List<(string,Definition)> variables = new List<(string, Definition)>();
        public override string ToString()
        {
            string str = "[" + GetType().Name + ":" + Name + ":(";
            for(int i = 0; i < variables.Count; i++)
            {
                str += variables[i].Item2.GetType().Name.Substring(2) +" " + variables[i].Item1;
                if (variables.Count > i + 1) str += ",";
            }
            str += "):";
            str += "{\n";
            for (int i = 0; i < innnerNodes.Count; i++)
            {
                if (i > 0 && innnerNodes[i - 1].Line < innnerNodes[i].Line) str += "\n"; 
                str += innnerNodes[i];
            }
            str += "\n}]\n";
            return str;
        }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out AddMethodNode node))
            {
                if (i + 1 < nodes.Count && nodes[i + 1].TryGetNode(out MethodNode name))
                {
                    node.Name =  name.Raw;
                    nodes.RemoveAt(i + 1);
                }
                int index = i + 1;
                if (IfNode.TryGetBracketSubInfo(ref index, ref nodes, out List<Node> variables))
                {
                    while(variables.Count > 1)
                    {
                        if (variables[0].GetType().IsSubclassOf(typeof(Definition)))
                        {
                            (variables[0] as Definition).variableName = variables[1].Raw;
                            node.variables.Add((variables[1].Raw,(variables[0] as Definition)));
                            variables.RemoveAt(1);
                            variables.RemoveAt(0);

                        }
                        else
                        {
                            variables.RemoveAt(0);
                        }
                    }
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
            if (!Interpretator.methods.ContainsKey(Name)) Interpretator.methods.Add(Name, this);
            else Interpretator.methods[Name] = this;
            
        }
    }
}
