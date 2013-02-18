﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.EventArgsExtended;

namespace WinterEngine.Toolset.GUI.Views
{
    public interface IViewControls
    {
        void RefreshControls();
        void UnloadControls();
        void LoadObject(object sender, GameObjectEventArgs e);
        void SaveObject(object sender, GameObjectEventArgs e);
    }
}