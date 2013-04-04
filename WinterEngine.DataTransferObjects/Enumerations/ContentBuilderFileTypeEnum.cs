using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum ContentBuilderFileTypeEnum
    {
        ExternalFile,   // File is somewhere on disk and NOT contained inside of a content package
        PackageFile     // File is contained inside of a content package
    }
}
