using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Game.Services
{
    public static class WinterEngineService
    {
        public static event EventHandler OnXNAUpdate;
        public static event EventHandler OnXNADraw;

        /// <summary>
        /// Hooks into the raw XNA update method. 
        /// This is called before FlatRedBall and internal XNA methods.
        /// </summary>
        public static void Update()
        {
            if (!Object.ReferenceEquals(OnXNAUpdate, null))
            {
                OnXNAUpdate(null, new EventArgs());
            }
        }

        /// <summary>
        /// Hooks into the raw XNA draw method.
        /// This is called before FlatRedBall and internal XNA methods.
        /// </summary>
        public static void Draw()
        {
            if (!Object.ReferenceEquals(OnXNADraw, null))
            {
                OnXNADraw(null, new EventArgs());
            }
        }

    }
}
