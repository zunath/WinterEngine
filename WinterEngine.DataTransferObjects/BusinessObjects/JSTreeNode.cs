using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    [Serializable]
    public class JSTreeNode
    {
        public string data { get; set; }
        public List<JSTreeNode> children { get; set; }
        public Dictionary<string, string> attr { get; set; }

        public JSTreeNode(string title)
        {
            this.data = title;
            
            children = new List<JSTreeNode>();
            attr = new Dictionary<string, string>();
        }
    }
}
