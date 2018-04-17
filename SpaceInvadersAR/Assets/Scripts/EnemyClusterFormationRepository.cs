using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class EnemyClusterFormationRepository : MonoBehaviour
    {
        private Dictionary<LevelDifficulty, Dictionary<EnemyClusterType, EnemyClusterFormationInfo>> collection;

        void Awake()
        {
            collection = new Dictionary<LevelDifficulty, Dictionary<EnemyClusterType, EnemyClusterFormationInfo>>();

            var items = GetComponentsInChildren<EnemyClusterFormationRepositoryItem>();
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var itemClusterInfo = item.enemyClusterFormationInfo;
                var clusterDictionary = new Dictionary<EnemyClusterType, EnemyClusterFormationInfo>();

                for (int j = 0; j < itemClusterInfo.Length; j++)
                {
                    var infoItem = itemClusterInfo[j];
                    clusterDictionary.Add(infoItem.enemyClusterType, infoItem);
                }

                collection.Add(item.levelDifficulty, clusterDictionary);
            }
        }

        public EnemyClusterFormationInfo GetEnemyClusterFormationInfo(LevelDifficulty levelDifficulty, EnemyClusterType enemyClusterType)
        {
            if (!collection.ContainsKey(levelDifficulty))
            {
                Debug.LogError(string.Format("There is no collection for the specified level difficulty {0}", levelDifficulty));
            }

            var levelCollection = collection[levelDifficulty];
            if (!levelCollection.ContainsKey(enemyClusterType))
            {
                Debug.LogError(string.Format("There is no collection for the specified enemy cluster type {0} difficulty {1}", enemyClusterType, levelDifficulty));
            }

            return levelCollection[enemyClusterType];
        }
    }
}
