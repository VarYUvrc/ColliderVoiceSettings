using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Varyu.ColliderVoiceSettings
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LocalVoiceController : UdonSharpBehaviour
    {   
        [SerializeField] bool IsVoiceMute;
        int _localPlayerID;

        void Start()
        {
            _localPlayerID = Networking.LocalPlayer.playerId;
            if (IsVoiceMute) SetAllVoicesZero();
            else SetAllVoicesDefault();
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.playerId != _localPlayerID) return;
            if (IsVoiceMute) SetAllVoicesZero();
            else SetAllVoicesDefault();
        }

        private void SetAllVoicesZero()
        {
            VRCPlayerApi[] players = VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);
            foreach (VRCPlayerApi player in players)
            {
                player.SetVoiceGain(0);
                player.SetVoiceDistanceNear(0);
                player.SetVoiceDistanceFar(0);
            }
        }
        
        private void SetAllVoicesDefault()
        {
            VRCPlayerApi[] players = VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);
            foreach (VRCPlayerApi player in players)
            {
                player.SetVoiceGain(15);
                player.SetVoiceDistanceNear(0);
                player.SetVoiceDistanceFar(25);
            }
        }
    }
}