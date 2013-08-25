using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class ContentPackageCreatorViewModel
    {
        [XmlIgnore]
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<CPCResource> ResourceList { get; set; }
        [XmlIgnore]
        public bool IsModified { get; set; }

        public ContentPackageCreatorViewModel()
        {
            this.FilePath = "";
            this.Name = "";
            this.Description = "";
            this.ResourceList = new ObservableCollection<CPCResource>();
            this.IsModified = false;
        }
    }
}
