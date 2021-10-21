using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expl0sLazyDialogues
{
    class ObjectPatches
    {
        internal static IMonitor IMonitor;
        internal static IModHelper IHelper;

        internal static void Initialize(IMonitor IMonitor, IModHelper IHelper)
        {
            ObjectPatches.IMonitor = IMonitor;
            ObjectPatches.IHelper = IHelper;
        }

        internal static bool receiveKeyPress_Prefix(DialogueBox __instance, Keys key)
        {
            if (key == Keys.Space)
            {
                __instance.receiveLeftClick(0, 0);
                return false;
            }

            return true;
        }

        internal static bool DialogeBox_String_Prefix(string dialogue)
        {
            Game1.addHUDMessage(new HUDMessage("string"));

            dialogue += "^^> Press Space to continue...";

            return true;
        }

        internal static bool DialogeBox_List_Prefix(List<string> dialogues)
        {
            Game1.addHUDMessage(new HUDMessage("List<string>"));

            for (int iCounter = 0; iCounter < dialogues.Count; iCounter++)
            {
                dialogues[iCounter] += "^^> Press Space to continue...";
            }

            return true;
        }

        internal static bool DialogeBox_Dialogue_Prefix(Dialogue dialogue)
        {
            Game1.addHUDMessage(new HUDMessage("Dialogue"));

            for (int iCounter = 0; iCounter < dialogue.dialogues.Count; iCounter++)
            {
                string temp = dialogue.dialogues[iCounter];
                string addOn = "^^> Press Space to continue...";
                string subTemp = temp.Substring(temp.Length - addOn.Length);
                if (subTemp.Equals(addOn) == false)
                {
                    dialogue.dialogues[iCounter] += addOn;
                }
            }
            
            return true;
        }

        internal static bool DialogeBox_Question_Prefix(string dialogue, List<Response> responses, int width)
        {
            Game1.addHUDMessage(new HUDMessage("Question"));

            for (int iCounter = 0; iCounter < responses.Count; iCounter++)
            {
                responses[iCounter].responseText = (iCounter + 1).ToString() + ". " + responses[iCounter].responseText;
            }

            return true;
        }
    }
}
