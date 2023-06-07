using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MonkeSpider.Behaviours
{
    internal class MainManager : MonoBehaviour
    {
        internal (bool IsLocal, Player player) OnlinePlayer => (GetComponent<PhotonView>().IsMine, GetComponent<PhotonView>().Owner);

        internal class PlayerData
        {
            internal Transform[] BodyContraints; // 0 = Head, 1 = Left Arm, 2 = Right Arm
            internal Vector3[] DefaultLocalPositions; // 0 = Head, 1 = Left Arm, 2 = Right Arm
            internal GameObject LocalVRRigHead; // idk why this is even in the game, just degrading performance

            internal PlayerData(Transform head, Transform leftArm, Transform rightArm, GameObject localVRRigHead)
            {
                BodyContraints = new Transform[3] { head, leftArm, rightArm };
                DefaultLocalPositions = new Vector3[3] { head.localPosition, leftArm.localPosition, rightArm.localPosition };
                LocalVRRigHead = localVRRigHead;
            }
        }

        internal PlayerData SavedPlayerData;
        internal bool InEffect;

        private void Start()
        {
            SavedPlayerData = new PlayerData(transform.Find("rig/body/head"), transform.Find("rig/body/shoulder.L"), transform.Find("rig/body/shoulder.R"), GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/head/"));
        }

        private void Update()
        {
            if (Main.Instance.enabled && Main.Instance.InModded)
                if (!InEffect)
                    SetOffset(true);
                else;
            else if (InEffect)
                SetOffset(false);
        }

        private void SetOffset(bool Activity)
        {
            InEffect = Activity;
            for (int i = 0; i < SavedPlayerData.BodyContraints.Length; i++)
                if (SavedPlayerData.BodyContraints[i].name == "head")
                    SavedPlayerData.BodyContraints[i].localPosition = Activity ? SavedPlayerData.BodyContraints[i].localPosition + new Vector3(0, -0.1f, .15f) : SavedPlayerData.DefaultLocalPositions[i];
                else
                    SavedPlayerData.BodyContraints[i].localPosition = Activity ? (SavedPlayerData.BodyContraints[i].localPosition - (Vector3.up * 0.25f + Vector3.forward * -0.1f)) : SavedPlayerData.DefaultLocalPositions[i];
            SavedPlayerData.LocalVRRigHead.SetActive(!Activity);
        }
    }
}