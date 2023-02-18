using ProgramLanguage.Nodes.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes
{
    public class Command : Node
    {
        public Command(Node node): base(node.Raw, node.Line, node.Id, node.Interpretator)
        {
            
        }
        public static bool Compress(ref int i, ref List<Node> nodes)
        {
            return false;
        }

        public void GetComparitor(ref List<Node> nodes)
        {

        }
        public void GetAssign(ref List<Node> nodes)
        {

        }

        protected void AritmeticCompressRec(ref List<Node> nodes)
        {
            BracketCompress(ref nodes);
            AndOrCompress(ref nodes);
            CompareCompress(ref nodes);
            MulDivCompress(ref nodes);
            AddSubCompress(ref nodes);

        }
        protected void AndOrCompress(ref List<Node> nodes)
        {
            if (nodes.Where(o => o.Raw == "&" || o.Raw == "|").Count() == 0) return;

            List<Node> left = new List<Node>();
            List<Node> right = new List<Node>();
            while (nodes.Count > 0 && nodes[0].Raw != "&" && nodes[0].Raw != "|")
            {
                left.Add(nodes[0]);
                nodes.RemoveAt(0);
            }
            while (nodes.Count > 1)
            {
                right.Add(nodes[1]);
                nodes.RemoveAt(1);
            }
            AritmeticCompressRec(ref left);
            AritmeticCompressRec(ref right);
            if (nodes[0].TryGetNode(out And and))
            {
                and.right = right[0];
                and.left = left[0];
            }
            else if (nodes[0].TryGetNode(out Or or))
            {
                or.right = right[0];
                or.left = left[0];
            }
        }
        protected void CompareCompress(ref List<Node> nodes)
        {
            if (nodes.Where(o =>
                o.Raw == "==" ||
                o.Raw == "!=" ||
                o.Raw == "<" ||
                o.Raw == ">" ||
                o.Raw == ">=" ||
                o.Raw == "<=").Count() == 0) return;

            List<Node> left = new List<Node>();
            List<Node> right = new List<Node>();
            while (nodes.Count > 0 &&
                nodes[0].Raw != "==" &&
                nodes[0].Raw != "!=" &&
                nodes[0].Raw != "<"  &&
                nodes[0].Raw != ">"  &&
                nodes[0].Raw != ">=" &&
                nodes[0].Raw != "<=")
            {
                left.Add(nodes[0]);
                nodes.RemoveAt(0);
            }
            while (nodes.Count > 1)
            {
                right.Add(nodes[1]);
                nodes.RemoveAt(1);
            }
            AritmeticCompressRec(ref left);
            AritmeticCompressRec(ref right);
            if (nodes[0].TryGetNode(out Equal equal))
            {
                equal.right = right[0];
                equal.left = left[0];
            }
            else if (nodes[0].TryGetNode(out NotEqual notEqual))
            {
                notEqual.right = right[0];
                notEqual.left = left[0];
            }
            else if (nodes[0].TryGetNode(out Less less))
            {
                less.right = right[0];
                less.left = left[0];
            }
            else if (nodes[0].TryGetNode(out More more))
            {
                more.right = right[0];
                more.left = left[0];
            }
            else if (nodes[0].TryGetNode(out MoreEqual moreEqual))
            {
                moreEqual.right = right[0];
                moreEqual.left = left[0];
            }
            else if (nodes[0].TryGetNode(out LessEqual lessEqual))
            {
                lessEqual.right = right[0];
                lessEqual.left = left[0];
            }
        }
        protected void BracketCompress(ref List<Node> nodes)
        {
            // set all () recursevly
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Raw == "(")
                {
                    int leftBrachetCount = 1;
                    int rightBrachetCount = 0;
                    List<Node> innerNodes = new List<Node>();
                    while (nodes.Count > i + 1 && leftBrachetCount > rightBrachetCount)
                    {
                        if (nodes[i + 1].Raw == ")")
                        {
                            rightBrachetCount++;
                            if (leftBrachetCount > rightBrachetCount)
                            {
                                innerNodes.Add(nodes[i]);
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
                            innerNodes.Add(nodes[i + 1]);
                            nodes.RemoveAt(i + 1);
                        }
                    }
                    Node result = new PNull(nodes[i]);
                    if (innerNodes.Count > 0)
                    {
                        AritmeticCompressRec(ref innerNodes);
                        result = innerNodes[0];
                    }
                    nodes[i] = result;
                }
            }
        }
        protected void MulDivCompress(ref List<Node> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].TryGetNode(out Div div))
                {
                    if (i > 0)
                    {
                        div.left = nodes[i - 1];
                        nodes.RemoveAt(i - 1);
                        i--;
                    }
                    else
                    {
                        div.left = new PNull(div);
                    }
                    if (nodes.Count > i + 1)
                    {
                        div.right = nodes[i + 1];
                        nodes.RemoveAt(i + 1);
                    }
                    else
                    {
                        div.right = new PNull(div);
                    }
                }
                if (nodes[i].TryGetNode(out Mul mul))
                {
                    if (i > 0)
                    {
                        mul.left = nodes[i - 1];
                        nodes.RemoveAt(i - 1);
                        i--;
                    }
                    else
                    {
                        mul.left = new PNull(mul);
                    }
                    if (nodes.Count > i + 1)
                    {
                        mul.right = nodes[i + 1];
                        nodes.RemoveAt(i + 1);
                    }
                    else
                    {
                        mul.right = new PNull(mul);
                    }
                }
            }
        }
        protected void AddSubCompress(ref List<Node> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].TryGetNode(out Add add))
                {
                    if (i > 0)
                    {
                        add.left = nodes[i - 1];
                        nodes.RemoveAt(i - 1);
                        i--;
                    }
                    else
                    {
                        add.left = new PNull(add);
                    }
                    if (nodes.Count > i + 1)
                    {
                        add.right = nodes[i + 1];
                        nodes.RemoveAt(i + 1);
                    }
                    else
                    {
                        add.right = new PNull(add);
                    }
                }
                if (nodes[i].TryGetNode(out Sub sub))
                {
                    if (i > 0)
                    {
                        sub.left = nodes[i - 1];
                        nodes.RemoveAt(i - 1);
                        i--;
                    }
                    else
                    {
                        sub.left = new PNull(sub);
                    }
                    if (nodes.Count > i + 1)
                    {
                        sub.right = nodes[i + 1];
                        nodes.RemoveAt(i + 1);
                    }
                    else
                    {
                        sub.right = new PNull(sub);
                    }
                }

            }
        }
    }
}
