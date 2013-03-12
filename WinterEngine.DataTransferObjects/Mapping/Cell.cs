using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("Cells")]
    public class Cell
    {
        #region Fields

        private int _cellID;
        private int _xPosition;
        private int _yPosition;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique ID number of the cell
        /// </summary>
        [Key]
        public int CellID
        {
            get { return _cellID; }
            set { _cellID = value; }
        }

        /// <summary>
        /// Gets or sets the X position of the cell.
        /// </summary>
        public int X
        {
            get { return _xPosition; }
            set { _xPosition = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the cell.
        /// </summary>
        public int Y
        {
            get { return _yPosition; }
            set { _yPosition = value; }
        }

        #endregion
    }
}
