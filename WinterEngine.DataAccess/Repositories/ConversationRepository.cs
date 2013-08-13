using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Repositories
{
    public class ConversationRepository : RepositoryBase, IResourceRepository<Conversation>
    {
        #region Constructors

        public ConversationRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Conversation> GetAll()
        {
            return Context.ConversationRepository.Get().ToList();
        }

        public void Add(Conversation conversation)
        {
            Context.ConversationRepository.Add(conversation);
        }

        public void Add(List<Conversation> conversationList)
        {
            Context.ConversationRepository.AddList(conversationList);
        }

        public void Update(Conversation conversation)
        {
            Context.ConversationRepository.Update(conversation);
        }

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

        public Conversation GetByID(int conversationID)
        {
            return Context.ConversationRepository.Get(x => x.ResourceID == conversationID).SingleOrDefault();
        }

        public bool Exists(Conversation conversation)
        {
            Conversation dbConversation = Context.ConversationRepository.Get(x => x.ResourceID == conversation.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbConversation, null);
        }

        public void Delete(Conversation conversation)
        {
            Context.ConversationRepository.Delete(conversation);
        }

        public List<Conversation> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.ConversationRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }
        
        /// <summary>
        /// Generates a hierarchy of categories containing conversations for use in tree views.
        /// </summary>
        /// <returns></returns>
        public List<JSTreeNode> GenerateJSTreeCategories()
        {
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectType == GameObjectTypeEnum.Area).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.VisibleName);
                List<Conversation> conversations = GetAllByResourceCategory(category);
                foreach (Conversation conversation in conversations)
                {
                    JSTreeNode childNode = new JSTreeNode(conversation.VisibleName);
                    categoryNode.Children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            return treeNodes;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
