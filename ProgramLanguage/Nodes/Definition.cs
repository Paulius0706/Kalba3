using ProgramLanguage.Nodes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes
{
    public class Definition : Node
    {
        public string variableName;
        public Definition(Node node) : base(node.Raw,node.Line,node.Id,node.Interpretator) { }
        public virtual Primitive GetPrimitive() { return null; }

        public override void Execute()
        {
            Interpretator.primitives.Add(variableName, GetPrimitive());
        }

        public override string ToString()
        {
            return "[Define " + GetType().Name.Substring(2)+":" + variableName + "]";
        }
    }
    public class PDInt : Definition
    {

        public PDInt(Node node) : base(node) { }
        public override Primitive GetPrimitive(){ return new PInt(); }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out PDInt node))
            {
                node.variableName = nodes[i + 1].Raw;
                return true;
            }
            return false;
        }
    }
    public class PDFloat : Definition
    {
        public PDFloat(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PFloat(); }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out PDFloat node))
            {
                node.variableName = nodes[i + 1].Raw;
                return true;
            }
            return false;
        }
    }
    public class PDBool : Definition
    {
        public PDBool(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PBool(); }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out PDBool node))
            {
                node.variableName = nodes[i + 1].Raw;
                return true;
            }
            return false;
        }
    }
    public class PDString : Definition
    {
        public PDString(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PString(); }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out PDString node))
            {
                node.variableName = nodes[i + 1].Raw;
                return true;
            }
            return false;
        }
    }
    public class PDChar : Definition
    {
        public PDChar(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PChar(); }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out PDChar node))
            {
                node.variableName = nodes[i + 1].Raw;
                return true;
            }
            return false;
        }
    }
    public class PDArray : Definition
    {
        public PDArray(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PArray(); }

        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            if (nodes[i].TryGetNode(out PDArray node))
            {
                node.variableName = nodes[i + 1].Raw;
                return true;
            }
            return false;
        }
    }
}
