using Microsoft.Win32;
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
        public override void AssignNewInterpretator(Interpretator interpretator)
        {
            left.Interpretator = interpretator;
            right.Interpretator = interpretator;
            left.AssignNewInterpretator(interpretator);
            right.AssignNewInterpretator(interpretator);
        }
    }
    public class And : Operation
    {
        public And() : base() { }
        public And(Node node) : base(node) { }

        public override void Execute()
        {
            left.Execute();
            right.Execute();

            if (left.result.GetResult() is bool && right.result.GetResult() is bool)
            {
                int intiger = 0;
                intiger += (bool)left.result.GetResult() ? 1 : 0;
                intiger += (bool)right.result.GetResult() ? 1 : 0;
                var node = new PBool();
                node.Rez = intiger == 2 ? true : false;
                node.Execute();
                result = node;
            }
            else
            {
                result = new PNull();
            }
        }

    }
    public class Or : Operation
    {
        public Or() : base() { }
        public Or(Node node) : base(node) { }

        public override void Execute()
        {
            left.Execute();
            right.Execute();

            if (left.result.GetResult() is bool && right.result.GetResult() is bool)
            {
                int intiger = 0;
                intiger += (bool)left.result.GetResult() ? 1 : 0;
                intiger += (bool)right.result.GetResult() ? 1 : 0;
                var node = new PBool();
                node.Rez = intiger > 0 ? true : false;
                node.Execute();
                result = node;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class Add : Operation
    {
        public Add() : base() { }
        public Add(Node node) : base(node) { }
        
        public override void Execute()
        {
            left.Execute();
            right.Execute();

            if (left.result.GetResult() is string || right.result.GetResult() is string)
            {
                string str = "";
                str += left.result.GetResult().ToString();
                str += right.result.GetResult().ToString();
                var node = new PString();
                node.Rez = str;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is float || right.result.GetResult() is float)
            {
                float flt = 0f;
                flt += float.Parse(left.result.GetResult().ToString());
                flt += float.Parse(right.result.GetResult().ToString());
                var node = new PFloat();
                node.Rez = flt;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is int || right.result.GetResult() is int)
            {
                int intiger = 0;
                intiger += (int)left.result.GetResult();
                intiger += (int)right.result.GetResult();
                var node = new PInt();
                node.Rez = intiger;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is bool || right.result.GetResult() is bool)
            {
                int intiger = 0;
                intiger += (bool)left.result.GetResult() ? 1 : 0;
                intiger += (bool)right.result.GetResult() ? 1 : 0;
                var node = new PBool();
                node.Rez = intiger == 1 || intiger != -1 ? true : false;
                node.Execute();
                result = node;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class Sub : Operation
    {
        public Sub() : base() { }
        public Sub(Node node) : base(node) { }

        public override void Execute()
        {
            left.Execute();
            right.Execute();
            if (left.result.GetResult() is float || right.result.GetResult() is float)
            {
                float flt = 0f;
                flt += float.Parse(left.result.GetResult().ToString());
                flt -= float.Parse(right.result.GetResult().ToString());
                var node = new PFloat();
                node.Rez = flt;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is int || right.result.GetResult() is int)
            {
                int intiger = 0;
                intiger += (int)left.result.GetResult();
                intiger -= (int)right.result.GetResult();
                var node = new PInt();
                node.Rez = intiger;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is bool || right.result.GetResult() is bool)
            {
                int intiger = 0;
                intiger += (bool)left.result.GetResult() ? 1 : 0;
                intiger -= (bool)right.result.GetResult() ? 1 : 0;
                var node = new PBool();
                node.Rez = intiger == 1 || intiger != -1 ? true : false;
                node.Execute();
                result = node;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class Mul : Operation
    {
        public Mul() : base() { }
        public Mul(Node node) : base(node) { }

        public override void Execute()
        {
            left.Execute();
            right.Execute();
            if (left.result.GetResult() is float || right.result.GetResult() is float)
            {
                float flt = 1f;
                flt *= float.Parse(left.result.GetResult().ToString());
                flt *= float.Parse(right.result.GetResult().ToString());
                var node = new PFloat();
                node.Rez = flt;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is int || right.result.GetResult() is int)
            {
                int intiger = 1;
                intiger *= (int)left.result.GetResult();
                intiger *= (int)right.result.GetResult();
                var node = new PInt();
                node.Rez = intiger;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is bool || right.result.GetResult() is bool)
            {
                int intiger = 1;
                intiger *= (bool)left.result.GetResult() ? 1 : 0;
                intiger *= (bool)right.result.GetResult() ? 1 : 0;
                var node = new PBool();
                node.Rez = intiger == 1 || intiger != -1 ? true : false;
                node.Execute();
                result = node;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class Div : Operation
    {
        public Div() : base() { }
        public Div(Node node) : base(node) { }

        public override void Execute()
        {
            left.Execute();
            right.Execute();
            if (left.result.GetResult() is float || right.result.GetResult() is float)
            {
                float flt = 1f;
                flt *= float.Parse(left.result.GetResult().ToString());
                float right1 = float.Parse(right.result.GetResult().ToString());
                if (right1 != 0) flt /= right1;
                else flt = flt > 0 ? int.MaxValue : flt < 0 ? int.MinValue : 0;
                var node = new PFloat();
                node.Rez = flt;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is int || right.result.GetResult() is int)
            {
                int intiger = 1;
                intiger *= (int)left.result.GetResult();
                int right1 = (int)right.result.GetResult();
                if (right1 != 0) intiger /= right1;
                else intiger = intiger > 0 ? int.MaxValue : intiger < 0 ? int.MinValue : 0;
                var node = new PInt();
                node.Rez = intiger;
                node.Execute();
                result = node;
            }
            else if (left.result.GetResult() is bool || right.result.GetResult() is bool)
            {
                int intiger = 1;
                intiger *= (bool)left.result.GetResult() ? 1 : 0;
                intiger *= (bool)right.result.GetResult() ? 1 : 0;
                var node = new PBool();
                node.Rez = !(intiger == 1 || intiger != -1 ? true : false);
                node.Execute();
                result = node;
            }
            else
            {
                result = new PNull();
            }
        }
    }
}
