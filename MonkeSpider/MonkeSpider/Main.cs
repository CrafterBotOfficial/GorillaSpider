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
            VERSION = "1.0.0",
            KEY = "spidermonke";
        internal static Main Instance;

        internal ManualLogSource manualLogSource => Logger;
        internal bool InModded;

        internal Main()
        {
            Instance = this;

            new HarmonyLib.Harmony(GUID).PatchAll();
        }

        private void Start()
        {
            new GameObject("Callbacks").AddComponent<Behaviours.Callbacks>();
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
        private void OnModdedGamemodeLeave()
        {
            Behaviours.MainManager.Managers[Photon.Pun.PhotonNetwork.LocalPlayer].SetOffset(false);
            InModded = false;
        }
        #endregion

        bool IsInitialized;
        public void OnEnable()
        {
            manualLogSource.LogInfo("Enabled!");
            if (!IsInitialized)
            {
                IsInitialized = true;
                return;
            }
            Behaviours.MainManager.Managers[Photon.Pun.PhotonNetwork.LocalPlayer].SetOffset(true);
        }
        public void OnDisable()
        {
            manualLogSource.LogInfo("Disabled!");
            Behaviours.MainManager.Managers[Photon.Pun.PhotonNetwork.LocalPlayer].SetOffset(false);
        }
    }
}
