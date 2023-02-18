using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes
{
    public class Definition : Node
    {
        public Definition(Node node) : base(node.Raw,node.Line,node.Id,node.Interpretator) { }
        public virtual Primitive GetPrimitive() { return null; }
    }
    public class PDInt : Definition
    {
        public PDInt(Node node) : base(node) { }
        public override Primitive GetPrimitive(){ return new PInt(); }
    }
    public class PDFloat : Definition
    {
        public PDFloat(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PFloat(); }
    }
    public class PDBool : Definition
    {
        public PDBool(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PBool(); }
    }
    public class PDString : Definition
    {
        public PDString(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PString(); }
    }
    public class PDChar : Definition
    {
        public PDChar(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PChar(); }
    }
    public class PDArray : Definition
    {
        public PDArray(Node node) : base(node) { }
        public override Primitive GetPrimitive() { return new PArray(); }
    }
}
