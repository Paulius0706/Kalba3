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
        public Primitive(Node node) : base(node.Raw, node.Line, node.Id, node.Interpretator){ }

        public override void Execute()
        {
            result = this;
            base.Execute();
        }
        public void FastExecute()
        {
            result = Interpretator.primitives[Raw];
        }
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

        
        public override object GetResult()
        {
            return primitive;
        }
        // add recursive search
        public override void Execute()
        {
            primitive = GetPrimitive(Interpretator, Raw);
            result = primitive;
        }
        
        public Primitive GetPrimitive(Interpretator interpretator,string name)
        {
            if (interpretator.primitives.ContainsKey(name))
            {
                interpretator.primitives[name].Execute();
                return interpretator.primitives[name];
            }
            else if(interpretator.parent is not null)
            {
                return GetPrimitive(interpretator.parent, name);
            }
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
        public int Rez { get; set; }
        public PInt() : base() { }
        public PInt(Node node, bool parseRaw = true) :base(node)
        {
            if(parseRaw) Rez = int.Parse(node.Raw);
        }
        public override object GetResult()
        {
            return Rez;
        }
    }
    public class PFloat : Primitive
    {
        public float Rez { get; set; }
        public PFloat() : base() { }
        public PFloat(Node node, bool parseRaw = true) : base(node)
        {
            if (parseRaw) Rez = float.Parse(node.Raw);
        }
        public override object GetResult()
        {
            return Rez;
        }
    }
    public class PBool : Primitive
    {
        public bool Rez { get; set; }
        public PBool() : base() { }
        public PBool(Node node, bool parseRaw = true) : base(node)
        {
            if (parseRaw) Rez = Raw == "true" ? true : false;
        }
        public override object GetResult()
        {
            return Rez;
        }
    }
    public class PString : Primitive
    {
        public string Rez { get; set; }
        public PString() : base() { }
        public PString(Node node, bool parseRaw = true) : base(node)
        {
            if (parseRaw) Rez = node.Raw.Replace("\"", "");
        }
        public override object GetResult()
        {
            return Rez;
        }
    }
    public class PChar : Primitive
    {
        public string Rez { get; set; }
        public PChar() : base() { }
        public PChar(Node node, bool parseRaw = true) : base(node)
        {
            if (parseRaw) Rez = node.Raw.Replace("\"", "");
        }
        public override object GetResult()
        {
            return Rez;
        }
    }
    public class PArray : Primitive
    {
        public int Rez { get; set; }
        public PArray() : base() { }
        public PArray(Node node, bool parseRaw = true) : base(node)
        {
            if (parseRaw) Rez = int.Parse(node.Raw);
        }
        public override object GetResult()
        {
            return Rez;
        }
    }

}
