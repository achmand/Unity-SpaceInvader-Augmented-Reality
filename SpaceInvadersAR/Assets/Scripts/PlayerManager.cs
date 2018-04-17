using System;
using System.Collections.Generic;
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

    public class PlayerSpawnedEventArgs : EventArgs
    {
        public int PlayerId { get; set; }
    }

    // TODO -> Reference to local player ??
    public sealed class PlayerManager : MonoBehaviour
    {
        private Dictionary<int, PlayerOwner> roomPlayerOwners;

        public PhotonPlayer LocalPhotonPlayer
        {
            get
            {
                return PhotonNetwork.player;
            }
        }

        public PlayerOwner LocalPlayerOwner
        {
            get
            {
                return roomPlayerOwners.ContainsKey(LocalPhotonPlayer.ID) ? roomPlayerOwners[LocalPhotonPlayer.ID] : null;
            }
        }

        void Awake()
        {
            roomPlayerOwners = new Dictionary<int, PlayerOwner>();
        }

        public void AddPlayer(int playerId, PlayerOwner playerOwner)
        {
            roomPlayerOwners.Add(playerId, playerOwner);
        }

        public PlayerOwner ResolvePlayerOwner(int playerId)
        {
            if (!roomPlayerOwners.ContainsKey(playerId))
            {
                return null;
            }

            return roomPlayerOwners[playerId];
        }

        //public static PlayerManager Instance;

        //private PhotonView photonView;
        //private Dictionary<string, PlayerStats> playersStats = new Dictionary<string, PlayerStats>();

        //private void Awake()
        //{
        //    Instance = this;
        //    photonView = GetComponent<PhotonView>();
        //}

        //public void AddPlayerStats(PhotonPlayer photonPlayer)
        //{
        //    var playerName = photonPlayer.NickName;
        //    if (!playersStats.ContainsKey(playerName))
        //    {
        //        var playerStat = new PlayerStats(photonPlayer, 100f);
        //        playersStats.Add(playerName, playerStat);
        //    }
        //}

        //public void ModifyHealth(PhotonPlayer photonPlayer, int value)
        //{

        //}
    }
}