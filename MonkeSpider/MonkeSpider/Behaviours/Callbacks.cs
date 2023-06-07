using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

namespace MonkeSpider.Behaviours
{
    internal class Callbacks : MonoBehaviourPunCallbacks
    {
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
            if (targetPlayer.IsLocal || !changedProps.TryGetValue(Main.KEY, out object value))
                return;
            try { Behaviours.MainManager.Managers.First(x => x.Key == targetPlayer).Value.SetOffset((bool)value); } catch { }
        }
    }
}
