using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class ConversationRepository : RepositoryBase, IGameObjectRepository<Conversation>
    {
        #region Constructors
        
        public ConversationRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a conversation to the database.
        /// </summary>
        /// <param name="conversation">The conversation to add to the database.</param>
        /// <returns></returns>
        public void Add(Conversation conversation)
        {
            Context.ConversationRepository.Add(conversation);
        }

        /// <summary>
        /// Adds a list of conversations to the database.
        /// </summary>
        /// <param name="conversationList">The list of conversations to add to the database.</param>
        public void Add(List<Conversation> conversationList)
        {
            Context.ConversationRepository.AddList(conversationList);
        }

        /// <summary>
        /// Updates an existing conversation in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newConversation">The new conversation that will replace the conversation with the matching resref.</param>
        public void Update(Conversation newConversation)
        {
            Context.ConversationRepository.Update(newConversation);
        }

        /// <summary>
        /// If a conversation with the same resref is in the database, it will be replaced with newConversation.
        /// If a conversation does not exist by newConversation's resref, it will be added to the database.
        /// </summary>
        /// <param name="conversation">The new conversation to upsert.</param>
        public void Upsert(Conversation conversation)
        {
            if (conversation.ResourceID <= 0)
            {
                Context.ConversationRepository.Add(conversation);
            }
            else
            {
                Context.ConversationRepository.Update(conversation);
            }
        }

        /// <summary>
        /// Deletes a conversation with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            Conversation conversation = Context.ConversationRepository.Get(c => c.Resref == resref).SingleOrDefault();
            Context.ConversationRepository.Delete(conversation);
        }

        /// <summary>
        /// Returns all of the conversations from the database.
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetAll()
        {
            return Context.ConversationRepository.Get().ToList();
        }

        /// <summary>
        /// Returns all of the conversations in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.ConversationRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the conversation with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Conversation GetByResref(string resref)
        {
            return Context.ConversationRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the conversations attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Conversation> conversationList = Context.ConversationRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.DeleteAll(conversationList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Conversation conversation = Context.ConversationRepository.Get(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(conversation, null);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing conversations for use in tree views.
        /// </summary>
        /// <returns></returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Conversations");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectTypeID == (int)GameObjectTypeEnum.Creature).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                List<Conversation> conversations = GetAllByResourceCategory(category);
                foreach (Conversation conversation in conversations)
                {
                    JSTreeNode childNode = new JSTreeNode(conversation.Name);
                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
