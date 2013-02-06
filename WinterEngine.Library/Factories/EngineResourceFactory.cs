using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Library.Factories
{
    public class EngineResourceFactory
    {
        /// <summary>
        /// Returns the path to a particular engine resource.
        /// Returns an empty string ("") if resource type cannot be found.
        /// </summary>
        /// <param name="resource">The engine resource type whose path to look for</param>
        /// <param name="returnExtension">If true, the extension will be part of the return value. If false, only the file path will be returned.</param>
        /// <returns></returns>
        public string GetResourcePath(EngineResourceEnum resource, bool returnExtension)
        {
            string path = "";

            switch (resource)
            {
                case EngineResourceEnum.Icon_InvalidDimensions:
                    path = "./Content/Icons/Icon_InvalidDimensions.xnb";
                    break;
                case EngineResourceEnum.Icon_NotPassable:
                    path = "./Content/Icons/Icon_NotPassable.xnb";
                    break;
                case EngineResourceEnum.Icon_Passable:
                    path = "./Content/Icons/Icon_Passable.xnb";
                    break;
            }

            if (!returnExtension)
            {
                path = Path.GetDirectoryName(path) + Path.GetFileNameWithoutExtension(path);
            }

            return path;
        }

    }
}
