using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Math
{
    public class Comparitor : Node
    {
        public Node left;
        public Node right;
        public Comparitor() : base() { }
        public Comparitor(Node node) : base(node.Raw, node.Line, node.Id, node.Interpretator) { }

        public override string ToString()
        {
            string str = "(";
            if (left is not null) str += left + "-";
            str += GetType().Name;
            if (left is not null) str += "-" + right;
            str += ")";
            return str;
        }
    }

    public class More : Comparitor
    {
        public More() : base() { }
        public More(Node node) : base(node) { }
    }
    public class Less : Comparitor
    {
        public Less() : base() { }
        public Less(Node node) : base(node) { }
    }
    public class Equal : Comparitor
    {
        public Equal() : base() { }
        public Equal(Node node) : base(node) { }
    }
    public class MoreEqual : Comparitor
    {
        public MoreEqual() : base() { }
        public MoreEqual(Node node) : base(node) { }
    }
    public class LessEqual : Comparitor
    {
        public LessEqual() : base() { }
        public LessEqual(Node node) : base(node) { }
    }
    public class NotEqual : Comparitor
    {
        public NotEqual() : base() { }
        public NotEqual(Node node) : base(node) { }
    }
}
