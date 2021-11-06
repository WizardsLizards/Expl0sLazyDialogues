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
        //No Messages = 0, Small Messages = 1, Long Messages = 2, (Symbol = 3)
        public int MessageMode { get; set; } = 2;

        //No Numbers = 0, Numbers = 1
        public int NumberMode { get; set; } = 1;

        //DialogueKeys default: Space
        public KeybindList DialogueKeys { get; set; } = KeybindList.Parse("Space");

        //InitiateDialogueKeys default: Space
        public KeybindList InitiateDialogueKeys { get; set; } = KeybindList.Parse("Space");

        //Initiate Dialogue with Dialogue Key
        public bool InitiateWithKey { get; set; } = true;

        //Initiates Dialogue even if target is 2 tiles away
        public bool CheckTwoSpaces { get; set; } = true;

        //Initiates Dialogue even if target is to the side of frontal tile
        public bool CheckToTheSidesOne { get; set; } = true;

        //Initiates Dialogue even if target is 2 tiles away to the side
        public bool CheckToTheSidesTwo { get; set; } = false;
    }
}