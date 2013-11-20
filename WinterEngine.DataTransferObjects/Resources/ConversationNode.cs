using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("ConversationNodes")]
    public class ConversationNode : GameResourceBase
    {
        #region Fields

        #endregion

        #region Properties

        public virtual int ParentResponseID { get; set; }
        [ForeignKey("ParentResponseID")]
        public virtual ConversationNode ParentResponse { get; set; }

        public virtual IEnumerable<ConversationNode> ChildResponses { get; set; }

        public virtual int ConversationID { get; set; }
        [ForeignKey("ConversationID")]
        public virtual Conversation ParentConversation { get; set; }

        public virtual ConversationNodeTypeEnum ResponseType { get; set; }
        public virtual string Text { get; set; }

        #endregion
    }
}
