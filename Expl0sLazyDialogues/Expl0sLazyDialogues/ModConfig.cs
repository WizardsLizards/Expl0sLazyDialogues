using StardewModdingAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expl0sLazyDialogues
{
    class ModConfig
    {
        //No Messages = 0, Small Messages = 1, Long Messages = 2
        public int MessageMode { get; set; } = 2;

        //No Numbers = 0, Numbers = 1
        public int NumberMode { get; set; } = 1;

        //DialogueKey default: Space
        public KeybindList DialogueKey { get; set; } = KeybindList.Parse("Space");

        //Initiate Dialogue with Dialogue Key
        public bool InitiateWithKey { get; set; } = true;
    }
}