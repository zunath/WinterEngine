using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Conversations")]
    public class Conversation : GameObjectBase
    {
        #region Fields
        
        #endregion

        #region Properties

        public virtual IEnumerable<ConversationNode> Responses { get; set; }

        #endregion

        #region Constructors

        public Conversation()
        {
        }

        public Conversation(bool instantiateLists)
        {
            if (instantiateLists)
            {
                LocalVariables = new List<LocalVariable>();
            }
            else
            {
            }
        }

        #endregion
    }
}
