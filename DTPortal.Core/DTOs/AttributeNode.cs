using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class AttributeNode
    {
        public string Name { get; set; }
        public List<AttributeNode> SubAttributes { get; set; }
        public AttributeNode()
        {
            SubAttributes = new List<AttributeNode>();
        }
    }
}
