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
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="IHelper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper IHelper)
        {
            #region setup
            var csHarmony = new Harmony(this.ModManifest.UniqueID);

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
        }
    }
}