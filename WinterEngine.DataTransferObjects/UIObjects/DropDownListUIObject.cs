using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.UIObjects
{
    public class DropDownListUIObject
    {
        public int ResourceID { get; set; }
        public string Name { get; set; }

        public DropDownListUIObject()
        {
        }

        public DropDownListUIObject(int resourceID, string name)
        {
            this.ResourceID = resourceID;
            this.Name = name;
        }
    }
}
