using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum GameTypeEnum: byte
    {
        Action = 1,
        Story = 2,
        RolePlay = 3,
        Team = 4,
        Melee = 5,
        Social = 6,
        Alternative = 7,
        PWAction = 8,
        PWStory = 9,
        Solo = 10
    }
}
