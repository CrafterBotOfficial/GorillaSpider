using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using Utilla;

namespace MonkeSpider
{
    [BepInPlugin(GUID, NAME, VERSION), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [ModdedGamemode, System.ComponentModel.Description("HauntedModMenu")]
    internal class Main : BaseUnityPlugin
    {
        internal const string
            GUID = "crafterbot.somedumbmonkegame.monkespider",
            NAME = "MonkeSpider",
            VERSION = "1.0.0";
        internal static Main Instance;

        internal ManualLogSource manualLogSource => Logger;
        internal bool InModded;

        internal Main()
        {
            Instance = this;

            new HarmonyLib.Harmony(GUID).PatchAll();
        }

#if DEBUG
        private void OnGUI()
        {
            if (!InModded)
                GUI.Window(50, new Rect(500, 50, 200, 45), Win, "Spider monke debug");
            void Win(int WindowID)
            {
                GorillaNetworking.GorillaComputer.instance.currentGameMode = "MODDED_CASUAL";
                if (GUILayout.Button("Join Modded"))
                    Utilla.Utils.RoomUtils.JoinPrivateLobby("crafterbot");
            }
        }
#endif

        #region Utilla callbacks
        [ModdedGamemodeJoin]
        private void OnModdedGamemodeJoin() =>
            InModded = true;
        [ModdedGamemodeLeave]
        private void OnModdedGamemodeLeave() =>
            InModded = false;
        #endregion

        public void OnEnable() =>
            manualLogSource.LogInfo("Enabled!");
        public void OnDisable() =>
            manualLogSource.LogInfo("Disabled!");
    }
}
