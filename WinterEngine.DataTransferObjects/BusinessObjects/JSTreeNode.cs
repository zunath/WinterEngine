using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    public class JSTreeNode
    {
        public string Data { get; set; }
        public List<JSTreeNode> Children { get; set; }

        public JSTreeNode(string title)
        {
            this.Data = title;
            
            Children = new List<JSTreeNode>();
        }
    }
}
