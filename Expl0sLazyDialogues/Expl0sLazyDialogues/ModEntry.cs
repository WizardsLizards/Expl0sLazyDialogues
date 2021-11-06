using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using HarmonyLib;
using System.Collections.Generic;

namespace Expl0sLazyDialogues
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        internal static IModHelper ModHelper;
        internal static ModConfig Configuration;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="IHelper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper IHelper)
        {
            #region setup
            ModHelper = IHelper;

            var csHarmony = new Harmony(this.ModManifest.UniqueID);

            Configuration = ModHelper.ReadConfig<ModConfig>();

            ObjectPatches.Initialize(this.Monitor, IHelper);
            #endregion

            #region Harmony patches
            //Patches the keyboard input, to receive 1-0 and space
            csHarmony.Patch(
               original: AccessTools.Method(typeof(StardewValley.Menus.DialogueBox), "receiveKeyPress"),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.receiveKeyPress_Prefix))
            );

            //multiple constructors for messageboxes, questionboxes, etc.
            csHarmony.Patch(
               original: AccessTools.Constructor(typeof(StardewValley.Menus.DialogueBox), new Type[] { typeof(string) }),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.DialogeBox_String_Prefix))
            );

            csHarmony.Patch(
               original: AccessTools.Constructor(typeof(StardewValley.Menus.DialogueBox), new Type[] { typeof(List<string>) }),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.DialogeBox_List_Prefix))
            );

            csHarmony.Patch(
               original: AccessTools.Constructor(typeof(StardewValley.Menus.DialogueBox), new Type[] { typeof(Dialogue) }),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.DialogeBox_Dialogue_Prefix))
            );

            csHarmony.Patch(
               original: AccessTools.Constructor(typeof(StardewValley.Menus.DialogueBox), new Type[] { typeof(string), typeof(List<Response>), typeof(int) }),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.DialogeBox_Question_Prefix))
            );
            #endregion

            #region events
            ModHelper.Events.Input.ButtonPressed += OnDialogueButtonPressed;
            #endregion
        }

        internal void OnDialogueButtonPressed(object sender, ButtonPressedEventArgs e)
        //internal static bool receiveKeyPress_Prefix(DialogueBox __instance, Keys key)
        {
            //TODO: Only run once ingame
            try
            {
                if(Context.IsWorldReady == true)
                {
                    string KeyName = e.Button.ToString();
                    List<string> KeyArray = ObjectPatches.GetKeyArrayInitiateDialogue();

                    if (KeyArray.Contains(KeyName))
                    {
                        Game1.addHUDMessage(new HUDMessage(Game1.player.FacingDirection.ToString()));

                        //Check that the dialogue can be initiated
                        if (true)
                        {
                            Vector2 PlayerLocation = Game1.player.getTileLocation();
                            //0 = up, 1 = right, 2 = down, 3 = left
                            int PlayerDirection = Game1.player.FacingDirection;

                            List<Vector2> TilesToCheck = new List<Vector2>();

                            //fill TilesToCheck

                            //NPCs first
                            foreach (NPC TempNPC in Game1.currentLocation.characters)
                            {
                                //Game1.currentLocation.isCharacterAtTile
                            }

                            //Object after
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ObjectPatches.IMonitor.Log($"Failed in {nameof(OnDialogueButtonPressed)}:\n{ex}", LogLevel.Error);
            }
        }
    }
}