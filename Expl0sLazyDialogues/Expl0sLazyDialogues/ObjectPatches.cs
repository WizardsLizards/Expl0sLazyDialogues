using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
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
            try
            {
                var KeyName = key.ToString();
                var KeyArray = ModEntry.Configuration.DialogueKey.ToString().Split(',').Select(x => x.Replace(" ", string.Empty)).ToList();

                //if (key == Keys.Space) //ModEntry.Configuration.DialogueKey - Possibly a list of keys -> split by ,
                if (KeyArray.Contains(KeyName))
                {
                    __instance.receiveLeftClick(0, 0);
                    return false;
                }

                int iSelectedResponse = -1;

                switch (key)
                {
                    case (Keys.D1):
                        iSelectedResponse = 0;
                        break;

                    case (Keys.D2):
                        iSelectedResponse = 1;
                        break;

                    case (Keys.D3):
                        iSelectedResponse = 2;
                        break;

                    case (Keys.D4):
                        iSelectedResponse = 3;
                        break;

                    case (Keys.D5):
                        iSelectedResponse = 4;
                        break;

                    case (Keys.D6):
                        iSelectedResponse = 5;
                        break;

                    case (Keys.D7):
                        iSelectedResponse = 6;
                        break;

                    case (Keys.D8):
                        iSelectedResponse = 7;
                        break;

                    case (Keys.D9):
                        iSelectedResponse = 8;
                        break;

                    case (Keys.D0):
                        iSelectedResponse = 9;
                        break;
                }

                if (iSelectedResponse == -1 || __instance.responses.Count <= iSelectedResponse)
                {
                    return true;
                }

                __instance.selectedResponse = iSelectedResponse;

                __instance.questionFinishPauseTimer = (Game1.eventUp ? 600 : 200);
                __instance.transitioning = true;
                __instance.transitionInitialized = false;
                __instance.transitioningBigger = true;
                if (__instance.characterDialogue == null)
                {
                    Game1.dialogueUp = false;
                    if (Game1.eventUp && Game1.currentLocation.afterQuestion == null)
                    {
                        Game1.playSound("smallSelect");
                        Game1.currentLocation.currentEvent.answerDialogue(Game1.currentLocation.lastQuestionKey, iSelectedResponse);
                        iSelectedResponse = -1;
                        ModEntry.ModHelper.Reflection.GetMethod(__instance, "tryOutro").Invoke();
                        return false;
                    }
                    if (Game1.currentLocation.answerDialogue(__instance.responses[iSelectedResponse]))
                    {
                        Game1.playSound("smallSelect");
                    }
                    iSelectedResponse = -1;
                    ModEntry.ModHelper.Reflection.GetMethod(__instance, "tryOutro").Invoke();
                    return false;
                }
                __instance.characterDialoguesBrokenUp.Pop();
                __instance.characterDialogue.chooseResponse(__instance.responses[iSelectedResponse]);
                __instance.characterDialoguesBrokenUp.Push("");
                Game1.playSound("smallSelect");

                return true;
            }
            catch (Exception ex)
            {
                IMonitor.Log($"Failed in {nameof(receiveKeyPress_Prefix)}:\n{ex}", LogLevel.Error);
                return true;
            }
        }

        internal static void DialogeBox_String_Prefix(ref string dialogue)
        {
            try
            {
                //Game1.addHUDMessage(new HUDMessage("string"));

                dialogue += GetAddOnString();

                //return true;
            }
            catch (Exception ex)
            {
                IMonitor.Log($"Failed in {nameof(DialogeBox_String_Prefix)}:\n{ex}", LogLevel.Error);
            }
        }

        internal static void DialogeBox_List_Prefix(List<string> dialogues)
        {
            try
            {
                //Game1.addHUDMessage(new HUDMessage("List<string>"));

                for (int iCounter = 0; iCounter < dialogues.Count; iCounter++)
                {
                    dialogues[iCounter] += GetAddOnString();
                }

                //return true;
            }
            catch (Exception ex)
            {
                IMonitor.Log($"Failed in {nameof(DialogeBox_List_Prefix)}:\n{ex}", LogLevel.Error);
            }
        }

        internal static void DialogeBox_Dialogue_Prefix(Dialogue dialogue)
        {
            try
            {
                //Game1.addHUDMessage(new HUDMessage("Dialogue"));

                for (int iCounter = 0; iCounter < dialogue.dialogues.Count; iCounter++)
                {
                    string temp = dialogue.dialogues[iCounter];
                    string addOn = GetAddOnString();
                    if (temp.Length >= addOn.Length)
                    {
                        string subTemp = temp.Substring(temp.Length - addOn.Length);
                        if (subTemp.Equals(addOn) == false)
                        {
                            dialogue.dialogues[iCounter] += addOn;
                        }
                    }
                }

                //return true;
            }
            catch (Exception ex)
            {
                IMonitor.Log($"Failed in {nameof(DialogeBox_Dialogue_Prefix)}:\n{ex}", LogLevel.Error);
            }
        }

        internal static void DialogeBox_Question_Prefix(string dialogue, List<Response> responses, int width)
        {
            try
            {
                //Game1.addHUDMessage(new HUDMessage("Question"));

                if (ModEntry.Configuration.NumberMode == 1)
                {
                    for (int iCounter = 0; iCounter < responses.Count; iCounter++)
                    {
                        responses[iCounter].responseText = (iCounter + 1).ToString() + ". " + responses[iCounter].responseText;
                    }
                }

                //return true;
            }
            catch (Exception ex)
            {
                IMonitor.Log($"Failed in {nameof(DialogeBox_Question_Prefix)}:\n{ex}", LogLevel.Error);
            }
        }

        internal static string GetAddOnString()
        {
            string Key = ModEntry.Configuration.DialogueKey.ToString();

            if (ModEntry.Configuration.MessageMode == 2)
            {
                return $"^^> Press {Key} to continue...";
            }
            else if (ModEntry.Configuration.MessageMode == 1)
            {
                return $"^^> {Key}...";
            }
            else
            {
                return "";
            }
        }
    }
}
