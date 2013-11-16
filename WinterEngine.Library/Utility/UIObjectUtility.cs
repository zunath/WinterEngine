using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;


namespace WinterEngine.Library.Utility
{
    public class UIObjectUtility : IUITreeObjectRepository
    {
        public JSTreeNode GenerateJSTreeHierarchy<T>() where T : GameObjectBase
        {
            throw new NotImplementedException();
        }

    }
}
