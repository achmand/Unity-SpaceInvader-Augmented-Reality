using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    //public sealed class PlayerStats
    //{
    //    public readonly PhotonPlayer PhotonPlayer;
    //    public float Health;

    //    public PlayerStats(PhotonPlayer photonPlayer, float health)
    //    {
    //        PhotonPlayer = photonPlayer;
    //        Health = health;
    //    }
    //}

    //public class PlayerSpawnedEventArgs : EventArgs
    //{
    //    public int PlayerId { get; set; }
    //}

    public sealed class PlayerManager : MonoBehaviour
    {
        public int TotalRoomPlayers { get { return roomPlayerOwners.Count; } }
        public int totalAlivePlayers; 

        private Dictionary<int, PlayerOwner> roomPlayerOwners;
        private UiManager uiManager;

        public PlayerOwner LocalPlayerOwner
        {
            get
            {
                return roomPlayerOwners.ContainsKey(PhotonNetwork.player.ID) ? roomPlayerOwners[PhotonNetwork.player.ID] : null;
            }
        }

        void Awake()
        {
            var components = GlobalReferenceManager.GlobalInstance;
            uiManager = components.uiManager;

            roomPlayerOwners = new Dictionary<int, PlayerOwner>();
        }

        public void AddPlayer(PlayerOwner playerOwner)
        {
            var playerId = playerOwner.PlayerId;
            roomPlayerOwners.Add(playerId, playerOwner);
            totalAlivePlayers++;

            //var isLocalPlayer = IsLocalPlayer(playerId);
            //if (isLocalPlayer)
            //{
            //    playerOwner.OnPlayerDied += PlayerOwner_OnPlayerDied;
            //}
        }

        public PlayerOwner ResolvePlayerOwner(int playerId)
        {
            if (!roomPlayerOwners.ContainsKey(playerId))
            {
                return null;
            }

            return roomPlayerOwners[playerId];
        }

        public bool IsLocalPlayer(int playerId)
        {
            if (LocalPlayerOwner == null)
            {
                return false; 
            }

            return LocalPlayerOwner.PlayerId == playerId;
        }

        public PlayerOwner GetRandomPlayerOwner()
        {
            var playerKeys = roomPlayerOwners.Keys.ToArray(); // TODO -> Have generic pools for arrays and lists !!!
            var randomId = playerKeys.GetRandomElement();
            if (!roomPlayerOwners.ContainsKey(randomId))
            {
                return null;
            }

            var randomPlayerOwner = roomPlayerOwners[randomId];
            if (!randomPlayerOwner.IsAlive) // TODO -> Temporary, must make sure to only select from alive player owners
            {
                return null;
            }

            return randomPlayerOwner;
        }

        public void PlayerDied(int playerId)
        {
            totalAlivePlayers--;
            var isLocalPlayer = IsLocalPlayer(playerId);
            if (isLocalPlayer)
            {
                uiManager.ShowGameOverPanel();
            }
        }

        //private void PlayerOwner_OnPlayerDied(object sender, EventArgs e)
        //{
        //    uiManager.ShowGameOverPanel();
        //}
    }
}