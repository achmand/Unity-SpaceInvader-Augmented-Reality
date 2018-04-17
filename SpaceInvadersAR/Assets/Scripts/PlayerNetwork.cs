using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class PlayerNetwork : MonoBehaviour
    {
        public static PlayerNetwork Instance;

        public string PlayerName { get; private set; }

        private PhotonView photonView;
        private int playersInGame = 0;

        private void Awake()
        {
            Instance = this;
            photonView = GetComponent<PhotonView>();
            PlayerName = "Dylan " + Random.Range(1000, 9999);

            PhotonNetwork.sendRate = 60;
            PhotonNetwork.sendRateOnSerialize = 30;

            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "ClientScene")
            {
                if (PhotonNetwork.isMasterClient)
                {
                    MasterLoadedGame();
                }
                else
                {
                    NonMasterLoadedGame();
                }

                GlobalReferenceManager.GlobalInstance.clientGameManager.StartGame();
            }
        }

        private void MasterLoadedGame()
        {
            photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
            photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
        }

        private void NonMasterLoadedGame()
        {
            photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        }

        [PunRPC]
        private void RPC_LoadGameOthers()
        {
            PhotonNetwork.LoadLevel("ClientScene");
        }

        [PunRPC]
        private void RPC_LoadedGameScene()
        {
            playersInGame++;
            if (playersInGame == PhotonNetwork.playerList.Length)
            {
                print("All players are in the game.");
                photonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
            }
        }

        [PunRPC]
        private void RPC_CreatePlayer()
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Client Player Owner"), Vector3.zero, Quaternion.identity, 0);
        }
    }
}