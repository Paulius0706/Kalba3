using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Math
{
    public class Operation : Node
    {
        public Node left;
        public Node right;
        public Operation() : base() { }
        public Operation(Node node) : base(node.Raw, node.Line, node.Id, node.Interpretator) { }

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
    public class And : Operation
    {
        public And() : base() { }
        public And(Node node) : base(node) { }
    }
    public class Or : Operation
    {
        public Or() : base() { }
        public Or(Node node) : base(node) { }
    }
    public class Add : Operation
    {
        public Add() : base() { }
        public Add(Node node) : base(node) { }
    }
    public class Sub : Operation
    {
        public Sub() : base() { }
        public Sub(Node node) : base(node) { }
    }
    public class Mul : Operation
    {
        public Mul() : base() { }
        public Mul(Node node) : base(node) { }
    }
    public class Div : Operation
    {
        public Div() : base() { }
        public Div(Node node) : base(node) { }
    }
}
