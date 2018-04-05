using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlayerNetwork : MonoBehaviour
    {
        public static PlayerNetwork Instance;
        public string PlayerName { get; private set; }
        private PhotonView PhotonView;
        private int PlayersInGame = 0;

        private void Awake()
        {
            Instance = this;
            PhotonView = GetComponent<PhotonView>();
            PlayerName = "Dylan " + Random.Range(1000, 9999);
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
            }
        }

        private void MasterLoadedGame()
        {
            PlayersInGame = 1;
            PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
        }

        private void NonMasterLoadedGame()
        {
            PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        }

        [PunRPC]
        private void RPC_LoadGameOthers()
        {
            PhotonNetwork.LoadLevel("ClientScene");
        }

        [PunRPC]
        private void RPC_LoadedGameScene()
        {
            PlayersInGame++;
            if (PlayersInGame == PhotonNetwork.playerList.Length)
            {
                print("All players are in the game.");
            }
        }
    }
}