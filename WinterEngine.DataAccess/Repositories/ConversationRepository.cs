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

        /// <summary>
        /// Adds a conversation to the database.
        /// </summary>
        /// <param name="conversation">The conversation to add to the database.</param>
        /// <returns></returns>
        public Conversation Add(Conversation conversation)
        {
            return _context.Conversations.Add(conversation);
        }

        /// <summary>
        /// Adds a list of conversations to the database.
        /// </summary>
        /// <param name="conversationList">The list of conversations to add to the database.</param>
        public void Add(List<Conversation> conversationList)
        {
            _context.Conversations.AddRange(conversationList);
        }

        /// <summary>
        /// If a conversation with the same resref is in the database, it will be replaced with newConversation.
        /// If a conversation does not exist by newConversation's resref, it will be added to the database.
        /// </summary>
        /// <param name="conversation">The new conversation to upsert.</param>
        public void Save(Conversation conversation)
        {
            if (conversation.ResourceID <= 0)
            {
                _context.Conversations.Add(conversation);
            }
            else
            {
                Update(conversation);
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

        /// <summary>
        /// Deletes a conversation from the database.
        /// </summary>
        /// <param name="conversation"></param>
        public void Delete(Conversation conversation)
        {
            this.Delete(conversation.ResourceID);
        }

        /// <summary>
        /// Deletes a conversation with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Conversation conversation = _context.Conversations.Where(c => c.ResourceID == resourceID).SingleOrDefault();
            _context.LocalVariables.RemoveRange(conversation.LocalVariables.ToList());
            _context.Conversations.Remove(conversation);
        }

        /// <summary>
        /// Returns all of the conversations from the database.
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetAll()
        {
            return _context.Conversations.ToList();
        }

        public Conversation GetByID(int resourceID)
        {
            return _context.Conversations.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
        }

        //todo: move this somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from conversation
        //                                        in Context.ConversationRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = conversation.Name,
        //                                            ResourceID = conversation.ResourceID
        //                                        }).ToList();

        //    return items;
        //}

        /// <summary>
        /// Returns all of the conversations in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetAllByResourceCategory(Category resourceCategory)
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
        /// Deletes all of the conversations attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Conversation> conversationList = _context.Conversations.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            _context.Conversations.RemoveRange(conversationList);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing conversations for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and conversations.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Conversations");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Conversation).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Conversation> conversations = _context.Conversations.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
                foreach (Conversation conversation in conversations)
                {
                    JSTreeNode childNode = new JSTreeNode(conversation.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(conversation.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(conversation.IsSystemResource));

                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
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

        //public int GetDefaultResourceID()
        //{
        //    Conversation defaultObject = _context.Conversations.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        #endregion

    }
}
