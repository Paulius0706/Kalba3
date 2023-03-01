using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Commands
{
    public class MethodNode : Command
    {
        public MethodNode(Node node) : base(node) { Name = node.Raw; }

        public string Name { get; set; }

        public List<Node> variables = new List<Node>();

        public override string ToString()
        {
            string str = "[" + GetType().Name+":"+ Name + ":(";
            for (int i = 0; i < variables.Count; i++)
            {
                str += variables[i];
            }
            str += ")]";
            return str;
        }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out MethodNode node))
            {
                int index = i + 1;
                if (MethodNode.TryGetBracketSubInfo(ref index, ref nodes, out List<Node> variables))
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
            List<Node> nodes = new List<Node>();
            Interpretator interpretator = new Interpretator();
            AddMethodNode addMethodNode = Interpretator.methods[Raw];
            for(int i = 0; i < addMethodNode.variables.Count; i++)
            {
                if (variables[i].GetType().Name == nameof(PVariable) )
                {
                    variables[i].Execute();
                    interpretator.primitives.Add(addMethodNode.variables[i].Item1, (variables[i] as PVariable).result);
                }
                else if (variables[i].Raw != ",")
                {
                    variables[i].Execute();
                    interpretator.primitives.Add(addMethodNode.variables[i].Item1, variables[i].result);
                }
            }
            foreach (var node in addMethodNode.innnerNodes)
            {
                nodes.Add(node);
            }
            foreach (var node in nodes)
            {
                node.Interpretator = interpretator;
                node.AssignNewInterpretator(interpretator);
            }
            interpretator.Execute(nodes);
        }
    }
}
