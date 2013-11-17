using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class ConversationRepository : IGameObjectRepository<Conversation>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors
        
        public ConversationRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Conversation InternalSave(Conversation conversation, bool saveChanges)
        {
            Conversation retConversation;
            if (conversation.ResourceID <= 0)
            {
                retConversation = _context.Conversations.Add(conversation);
            }
            else
            {
                retConversation = _context.Conversations.SingleOrDefault(x => x.ResourceID == conversation.ResourceID);
                if (retConversation == null) return null;
                _context.Entry(retConversation).CurrentValues.SetValues(conversation);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retConversation;
        }

        /// <summary>
        /// If a conversation with the same resref is in the database, it will be replaced with newConversation.
        /// If a conversation does not exist by newConversation's resref, it will be added to the database.
        /// </summary>
        /// <param name="conversation">The new conversation to upsert.</param>
        public Conversation Save(Conversation conversation)
        {
            return InternalSave(conversation, true);
        }

        public void Save(IEnumerable<Conversation> entityList)
        {
            if(entityList != null)
            {
                foreach(var conv in entityList)
                {
                    InternalSave(conv, false);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing conversation in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newConversation">The new conversation that will replace the conversation with the matching resref.</param>
        public void Update(Conversation newConversation)
        {
            Conversation dbConversation;
            if (newConversation.ResourceID <= 0)
            {
                dbConversation = _context.Conversations.Where(x => x.Resref == newConversation.Resref).SingleOrDefault();
            }
            else
            {
                dbConversation = _context.Conversations.Where(x => x.ResourceID == newConversation.ResourceID).SingleOrDefault();
            }
            if (dbConversation == null) return;

            foreach (LocalVariable variable in newConversation.LocalVariables)
            {
                variable.GameObjectBaseID = newConversation.ResourceID;
            }

            _context.Entry(dbConversation).CurrentValues.SetValues(newConversation);
            _context.LocalVariables.RemoveRange(dbConversation.LocalVariables.ToList());
            _context.LocalVariables.AddRange(newConversation.LocalVariables.ToList());
        }

        private void DeleteInternal(Conversation conversation, bool saveChanges = true)
        {
            var dbConversation = _context.Conversations.SingleOrDefault(x => x.ResourceID == conversation.ResourceID);
            if (dbConversation == null) return;

            _context.Conversations.Remove(dbConversation);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Conversation conversation)
        {
            DeleteInternal(conversation);
        }

        public void Delete(int resourceID)
        {
            var conversation = _context.Conversations.Find(resourceID);
            DeleteInternal(conversation);
        }

        public void Delete(IEnumerable<Conversation> conversationList)
        {
            foreach (var conversation in conversationList)
            {
                DeleteInternal(conversation, false);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all of the conversations from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Conversation> GetAll()
        {
            return _context.Conversations.ToList();
        }

        public Conversation GetByID(int resourceID)
        {
            return _context.Conversations.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        /// <summary>
        /// Returns all of the conversations in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Conversation> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Conversations.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the conversation with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Conversation GetByResref(string resref)
        {
            return _context.Conversations.Where(x => x.Resref == resref).SingleOrDefault();
        }        

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Conversation conversation = _context.Conversations.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(conversation, null);
        }

        #endregion

    }
}
