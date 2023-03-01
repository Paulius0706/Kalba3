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

        public override void Execute()
        {
            result = new PBool();

            left.Execute();
            right.Execute();

            if (left.result.TryGetNode(out PBool a1) && right.result.TryGetNode(out PBool b1))
            {
                a1.Execute();
                b1.Execute();
                (result as PBool).Rez = (a1.Rez ? 1 : 0) > (b1.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PInt a2) && right.result.TryGetNode(out PBool b2))
            {
                a2.Execute();
                b2.Execute();
                (result as PBool).Rez = a2.Rez > (b2.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PFloat a3) && right.result.TryGetNode(out PBool b3))
            {
                a3.Execute();
                b3.Execute();
                (result as PBool).Rez = a3.Rez > (b3.Rez ? 1 : 0);
            }

            else if (left.result.TryGetNode(out PInt a5) && right.result.TryGetNode(out PInt b5))
            {
                a5.Execute();
                b5.Execute();
                (result as PBool).Rez = a5.Rez > b5.Rez;
            }
            else if (left.result.TryGetNode(out PFloat a6) && right.result.TryGetNode(out PInt b6))
            {
                a6.Execute();
                b6.Execute();
                (result as PBool).Rez = a6.Rez > b6.Rez;
            }

            else if (left.result.TryGetNode(out PFloat a8) && right.result.TryGetNode(out PFloat b8))
            {
                a8.Execute();
                b8.Execute();
                (result as PBool).Rez = a8.Rez > b8.Rez;
            }
            else 
            { 
                result = new PNull(); 
            }
        }
    }
    public class Less : Comparitor
    {
        public Less() : base() { }
        public Less(Node node) : base(node) { }

        public override void Execute()
        {
            result = new PBool();

            left.Execute();
            right.Execute();

            if (left.result.TryGetNode(out PBool a1) && right.result.TryGetNode(out PBool b1))
            {
                a1.Execute();
                b1.Execute();
                (result as PBool).Rez = (a1.Rez ? 1 : 0) < (b1.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PInt a2) && right.result.TryGetNode(out PBool b2))
            {
                a2.Execute();
                b2.Execute();
                (result as PBool).Rez = a2.Rez < (b2.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PFloat a3) && right.result.TryGetNode(out PBool b3))
            {
                a3.Execute();
                b3.Execute();
                (result as PBool).Rez = a3.Rez < (b3.Rez ? 1 : 0);
            }

            else if (left.result.TryGetNode(out PInt a5) && right.result.TryGetNode(out PInt b5))
            {
                a5.Execute();
                b5.Execute();
                (result as PBool).Rez = a5.Rez < b5.Rez;
            }
            else if (left.result.TryGetNode(out PFloat a6) && right.result.TryGetNode(out PInt b6))
            {
                a6.Execute();
                b6.Execute();
                (result as PBool).Rez = a6.Rez < b6.Rez;
            }

            else if (left.result.TryGetNode(out PFloat a8) && right.result.TryGetNode(out PFloat b8))
            {
                a8.Execute();
                b8.Execute();
                (result as PBool).Rez = a8.Rez < b8.Rez;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class Equal : Comparitor
    {
        public Equal() : base() { }
        public Equal(Node node) : base(node) { }

        public override void Execute()
        {
            result = new PBool();

            left.Execute();
            right.Execute();

            if (left.result.TryGetNode(out PBool a1) && right.result.TryGetNode(out PBool b1))
            {
                a1.Execute();
                b1.Execute();
                (result as PBool).Rez = (a1.Rez ? 1 : 0) == (b1.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PInt a2) && right.result.TryGetNode(out PBool b2))
            {
                a2.Execute();
                b2.Execute();
                (result as PBool).Rez = a2.Rez == (b2.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PFloat a3) && right.result.TryGetNode(out PBool b3))
            {
                a3.Execute();
                b3.Execute();
                (result as PBool).Rez = a3.Rez == (b3.Rez ? 1 : 0);
            }

            else if (left.result.TryGetNode(out PInt a5) && right.result.TryGetNode(out PInt b5))
            {
                a5.Execute();
                b5.Execute();
                (result as PBool).Rez = a5.Rez == b5.Rez;
            }
            else if (left.result.TryGetNode(out PFloat a6) && right.result.TryGetNode(out PInt b6))
            {
                a6.Execute();
                b6.Execute();
                (result as PBool).Rez = a6.Rez == b6.Rez;
            }

            else if (left.result.TryGetNode(out PFloat a8) && right.result.TryGetNode(out PFloat b8))
            {
                a8.Execute();
                b8.Execute();
                (result as PBool).Rez = a8.Rez == b8.Rez;
            }

            else if(left.result.TryGetNode(out PString a9) && right.result.TryGetNode(out PString b9))
            {
                a9.Execute();
                b9.Execute();
                (result as PBool).Rez = a9.Rez == b9.Rez;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class MoreEqual : Comparitor
    {
        public MoreEqual() : base() { }
        public MoreEqual(Node node) : base(node) { }

        public override void Execute()
        {
            result = new PBool();

            left.Execute();
            right.Execute();

            if (left.result.TryGetNode(out PBool a1) && right.result.TryGetNode(out PBool b1))
            {
                a1.Execute();
                b1.Execute();
                (result as PBool).Rez = (a1.Rez ? 1 : 0) >= (b1.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PInt a2) && right.result.TryGetNode(out PBool b2))
            {
                a2.Execute();
                b2.Execute();
                (result as PBool).Rez = a2.Rez >= (b2.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PFloat a3) && right.result.TryGetNode(out PBool b3))
            {
                a3.Execute();
                b3.Execute();
                (result as PBool).Rez = a3.Rez >= (b3.Rez ? 1 : 0);
            }

            else if (left.result.TryGetNode(out PInt a5) && right.result.TryGetNode(out PInt b5))
            {
                a5.Execute();
                b5.Execute();
                (result as PBool).Rez = a5.Rez >= b5.Rez;
            }
            else if (left.result.TryGetNode(out PFloat a6) && right.result.TryGetNode(out PInt b6))
            {
                a6.Execute();
                b6.Execute();
                (result as PBool).Rez = a6.Rez >= b6.Rez;
            }

            else if (left.result.TryGetNode(out PFloat a8) && right.result.TryGetNode(out PFloat b8))
            {
                a8.Execute();
                b8.Execute();
                (result as PBool).Rez = a8.Rez >= b8.Rez;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class LessEqual : Comparitor
    {
        public LessEqual() : base() { }
        public LessEqual(Node node) : base(node) { }

        public override void Execute()
        {
            result = new PBool();

            left.Execute();
            right.Execute();

            if (left.result.TryGetNode(out PBool a1) && right.result.TryGetNode(out PBool b1))
            {
                a1.Execute();
                b1.Execute();
                (result as PBool).Rez = (a1.Rez ? 1 : 0) <= (b1.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PInt a2) && right.result.TryGetNode(out PBool b2))
            {
                a2.Execute();
                b2.Execute();
                (result as PBool).Rez = a2.Rez <= (b2.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PFloat a3) && right.result.TryGetNode(out PBool b3))
            {
                a3.Execute();
                b3.Execute();
                (result as PBool).Rez = a3.Rez <= (b3.Rez ? 1 : 0);
            }

            else if (left.result.TryGetNode(out PInt a5) && right.result.TryGetNode(out PInt b5))
            {
                a5.Execute();
                b5.Execute();
                (result as PBool).Rez = a5.Rez <= b5.Rez;
            }
            else if (left.result.TryGetNode(out PFloat a6) && right.result.TryGetNode(out PInt b6))
            {
                a6.Execute();
                b6.Execute();
                (result as PBool).Rez = a6.Rez <= b6.Rez;
            }

            else if (left.result.TryGetNode(out PFloat a8) && right.result.TryGetNode(out PFloat b8))
            {
                a8.Execute();
                b8.Execute();
                (result as PBool).Rez = a8.Rez <= b8.Rez;
            }
            else
            {
                result = new PNull();
            }
        }
    }
    public class NotEqual : Comparitor
    {
        public NotEqual() : base() { }
        public NotEqual(Node node) : base(node) { }

        public override void Execute()
        {
            result = new PBool();

            left.Execute();
            right.Execute();

            if (left.result.TryGetNode(out PBool a1) && right.result.TryGetNode(out PBool b1))
            {
                a1.Execute();
                b1.Execute();
                (result as PBool).Rez = (a1.Rez ? 1 : 0) != (b1.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PInt a2) && right.result.TryGetNode(out PBool b2))
            {
                a2.Execute();
                b2.Execute();
                (result as PBool).Rez = a2.Rez != (b2.Rez ? 1 : 0);
            }
            else if (left.result.TryGetNode(out PFloat a3) && right.result.TryGetNode(out PBool b3))
            {
                a3.Execute();
                b3.Execute();
                (result as PBool).Rez = a3.Rez != (b3.Rez ? 1 : 0);
            }

            else if (left.result.TryGetNode(out PInt a5) && right.result.TryGetNode(out PInt b5))
            {
                a5.Execute();
                b5.Execute();
                (result as PBool).Rez = a5.Rez != b5.Rez;
            }
            else if (left.result.TryGetNode(out PFloat a6) && right.result.TryGetNode(out PInt b6))
            {
                a6.Execute();
                b6.Execute();
                (result as PBool).Rez = a6.Rez != b6.Rez;
            }

            else if (left.result.TryGetNode(out PFloat a8) && right.result.TryGetNode(out PFloat b8))
            {
                a8.Execute();
                b8.Execute();
                (result as PBool).Rez = a8.Rez != b8.Rez;
            }

            else if (left.result.TryGetNode(out PString a9) && right.result.TryGetNode(out PString b9))
            {
                a9.Execute();
                b9.Execute();
                (result as PBool).Rez = a9.Rez != b9.Rez;
            }
            else
            {
                result = new PNull();
            }
        }
    }
}
