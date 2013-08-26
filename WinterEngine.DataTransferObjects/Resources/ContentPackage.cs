using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [Table("ContentPackages")]
    public class ContentPackage : GameResourceBase
    {
        #region Fields

        private string _contentPackagePath;
        private string _fileName;
        private string _description;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the content package path.
        /// </summary>
        [NotMapped]
        public string ContentPackagePath
        {
            get { return _contentPackagePath; }
            set { _contentPackagePath = value; }
        }

        /// <summary>
        /// Gets or sets the name of the physical file. This includes the file extension.
        /// </summary>
        [MaxLength(4000)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = Path.GetFileName(value); }
        }

        /// <summary>
        /// Gets or sets the description of the content package.
        /// </summary>
        [MaxLength(4000)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region Constructors

        public ContentPackage()
        {
            this.ResourceType = ResourceTypeEnum.ContentPackage;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a unique hash for this object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            // Reference: http://stackoverflow.com/questions/892618/create-a-hashcode-of-two-numbers
            int hashPrime = 23 * 31;
            return hashPrime + FileName.GetHashCode();
        }

        /// <summary>
        /// Returns true if two ContentPackage objects are the same.
        /// Returns false if they are not the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ContentPackage comparedObject = obj as ContentPackage;

            if (Object.ReferenceEquals(comparedObject, null))
            {
                return false;
            }
            else if (this.GetHashCode() == comparedObject.GetHashCode())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

    }
}
