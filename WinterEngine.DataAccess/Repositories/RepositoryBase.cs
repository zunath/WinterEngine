using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataAccess.Contexts;

namespace WinterEngine.DataAccess.Repositories
{
    //public class RepositoryBase : IRepository
    //{
    //    #region Fields

    //    protected ModuleDataContext _context;
    //    private bool _autoSaveChanges;

    //    #endregion

    //    #region Properties

    //    /// <summary>
    //    /// Gets or sets whether the repository will save all changes before being disposed.
    //    /// This is used only if the repository is called in a using statement.
    //    /// </summary>
    //    public bool AutoSaveChanges
    //    {
    //        get { return _autoSaveChanges; }
    //        set { _autoSaveChanges = value; }
    //    }

    //    #endregion

    //    #region Constructors

    //    public RepositoryBase(ModuleDataContext context, bool autoSave)
    //    {
    //        if (context == null) throw new ArgumentNullException("DbContext");
    //        _context = context;
    //        _autoSaveChanges = autoSave;
    //    }

    //    #endregion
    //}
}
