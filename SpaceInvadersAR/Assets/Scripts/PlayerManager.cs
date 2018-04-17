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

        public int TotalRoomPlayers { get { return roomPlayerOwners.Count; } }

        public PlayerOwner LocalPlayerOwner
        {
            get
            {
                return roomPlayerOwners.ContainsKey(PhotonNetwork.player.ID) ? roomPlayerOwners[PhotonNetwork.player.ID] : null;
            }
        }

        void Awake()
        {
            roomPlayerOwners = new Dictionary<int, PlayerOwner>();
        }

        public void AddPlayer(PlayerOwner playerOwner)
        {
            roomPlayerOwners.Add(playerOwner.PlayerId, playerOwner);
        }

        public PlayerOwner ResolvePlayerOwner(int playerId)
        {
            if (!roomPlayerOwners.ContainsKey(playerId))
            {
                return null;
            }

            return roomPlayerOwners[playerId];
        }

        //public PlayerOwner GetRandomPlayerOwner()
        //{
        //    if (PlayerNetwork.Instance.playersInGame == TotalRoomPlayers)
        //    {
        //        var keys = roomPlayerOwners.Keys.ToArray();
        //        var keyPosition = UnityEngine.Random.Range(0, keys.Length);
        //        var chosenKey = keys[keyPosition];

        //        return roomPlayerOwners.ContainsKey(chosenKey) ? roomPlayerOwners[chosenKey] : null;
        //    }

        //    return null;
        //}

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