﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    // Content Package Creator (CPC) Resource
    public class CPCResource
    {
        [XmlIgnore]
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public ContentPackageResourceTypeEnum ResourceType { get; set; }
        [XmlIgnore]
        public bool IsInPackage { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
