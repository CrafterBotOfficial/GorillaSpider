using HarmonyLib;
using Photon.Pun;

namespace MonkeSpider
{
    [HarmonyPatch]
    internal class Patches
    {
        [HarmonyPatch(typeof(VRRig), "Start"), HarmonyPostfix]
        private static void Hook_VRRig_Start(VRRig __instance)
        {
#if DEBUG
            Main.Instance.InModded = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED");
#endif

            Main.Instance.manualLogSource.LogInfo("A new VRRig has been created" + " | In modded : " + Main.Instance.InModded);
            if (Main.Instance.InModded && __instance.TryGetComponent(out PhotonView component) && component.Owner.IsLocal)
            {
                Main.Instance.manualLogSource.LogInfo("Adding MainManager to rig!");
                __instance.gameObject.AddComponent<Behaviours.MainManager>();
            }
        }
    }
}
