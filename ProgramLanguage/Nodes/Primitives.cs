using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes
{
    public class Primitive : Node
    {
        public Primitive():base(){}
        public Primitive(Node node) : base(node.Raw, node.Line, node.Id, node.Interpretator){  }

        public override string ToString()
        {
            return "[" + GetType().Name[1] +":" + Raw +"]";
        }
    }
    public class PVariable : Primitive
    {
        public Primitive primitive = new Primitive();
        public string Path { get; set; }
        public PVariable(Node node, string path) : base(node)
        {
            Path = path;
        }
        public PVariable(Node node) : base(node) 
        {
            Path = node.Raw;
        }
        private Node FindPrimitive()
        {
            return FindPrimitiveRec(Raw);
        }
        private Node FindPrimitiveRec(string name)
        {
            return null;
        }
    }
    public class PNull : Primitive
    {
        public PNull(): base(){}
        public PNull(Node node) : base(node) { }
        public override object GetResult(){ return null; }
    }
    public class PInt : Primitive
    {
        public int Number { get; set; }
        public PInt() : base() { }
        public PInt(Node node, bool parseRaw = false):base(node)
        {
            if(parseRaw) Number = int.Parse(node.Raw);
        }
        public override object GetResult()
        {
            return Number;
        }
    }
    public class PFloat : Primitive
    {
        public float Number { get; set; }
        public PFloat() : base() { }
        public PFloat(Node node, bool parseRaw = false) : base(node)
        {
            if (parseRaw) Number = float.Parse(node.Raw);
        }
        public override object GetResult()
        {
            return Number;
        }
    }
    public class PBool : Primitive
    {
        public bool Boolean { get; set; }
        public PBool() : base() { }
        public PBool(Node node, bool parseRaw = false) : base(node)
        {
            if (parseRaw) Boolean = Raw == "true" ? true : false;
        }
        public override object GetResult()
        {
            return Boolean;
        }
    }
    public class PString : Primitive
    {
        public string Str { get; set; }
        public PString() : base() { }
        public PString(Node node, bool parseRaw = false) : base(node)
        {
            if (parseRaw) Str = node.Raw.Replace("\"", "");
        }
        public override object GetResult()
        {
            return Str;
        }
    }
    public class PChar : Primitive
    {
        public string Str { get; set; }
        public PChar() : base() { }
        public PChar(Node node, bool parseRaw = false) : base(node)
        {
            if (parseRaw) Str = node.Raw.Replace("\"", "");
        }
        public override object GetResult()
        {
            return Str;
        }
    }
    public class PArray : Primitive
    {
        public int Number { get; set; }
        public PArray() : base() { }
        public PArray(Node node, bool parseRaw = false) : base(node)
        {
            if (parseRaw) Number = int.Parse(node.Raw);
        }
        public override object GetResult()
        {
            return Number;
        }
    }

}
