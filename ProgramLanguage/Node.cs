using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage
{
    public class Node
    {
        public Interpretator Interpretator { get; set; }
        public readonly int Id;
        public string Raw { get; private set; }
        public int Line { get; private set; }
        public string Enviroment { get; set; }
        public Node() { }
        public Node(string raw, int line, int id, Interpretator interpretator)
        {
            this.Id = id;
            Raw = raw;
            Line = line;
            Interpretator = interpretator;
        }
        public Node Rewrite()
        {

            return null;
        }
        public virtual object GetResult()
        {
            return null;
        }
        public virtual void Execute()
        {

        }
        public override string ToString()
        {
            return "[" + GetType().Name + ":" + Raw + "]";
        }
        public T GetNode<T>() where T : Node
        {
            return (T)this;
        }
        public bool TryGetNode<T>(out T node) where T : Node
        {
            node = null;
            if (!IsNode<T>()) return false; 
            node = GetNode<T>();
            return true;
        }
        public bool IsNode<T>() where T : Node
        {
            return typeof(T).Name == this.GetType().Name;
        }
    }
}
