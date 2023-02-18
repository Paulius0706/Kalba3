using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.Nodes.Helpers
{
    public class Helper : Node
    {
        public Helper() : base() { }
        public Helper(Node node) : base(node.Raw, node.Line, node.Id, node.Interpretator) { }
    }
}
