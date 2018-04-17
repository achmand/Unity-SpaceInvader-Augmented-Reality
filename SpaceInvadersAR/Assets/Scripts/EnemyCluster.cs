using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class EnemyCluster : MonoBehaviour
    {
        public Dictionary<int, EnemyBase> currentEnemyCluster;

        public float moveSpeed;
        public float yAxisBoundary;

        [HideInInspector]
        public bool createNewCluster;
        public EasingTypes spawnClusterScaleEasingType;
        public float spawnTimeInSeconds;

        public bool SpawningCluster { get { return scaleEasingApplier.IsEasing; } }

        //public bool debug_DestroyCluster;

        private PhotonView photonView;
        private FloatEasingApplier scaleEasingApplier;
        private List<EnemyClusterRow> currentEnemyClusterRows;

        private EnemyPoolManager enemyPoolManager;
        private EnemyClusterFormationRepository enemyClusterFormationRepository;

        private const float heightSpacing = 0.07f;
        private const float widthSpacing = 0.07f;

        private int currentClusterDeadCounter;
        private int currentClusterCounter;

        private bool initialized;

        private void FindReferences()
        {
            var globalComponents = GlobalReferenceManager.GlobalInstance;
            enemyPoolManager = globalComponents.enemyPoolManager;
            enemyClusterFormationRepository = globalComponents.enemyClusterFormationRepository;
        }

        public void Initialize()
        {
            FindReferences();
            currentEnemyCluster = new Dictionary<int, EnemyBase>();
            currentEnemyClusterRows = new List<EnemyClusterRow>();
            scaleEasingApplier = new FloatEasingApplier();
            initialized = true;
        }

        private void Update()
        {
            if (!initialized)
            {
                return;
            }

            //if (debug_DestroyCluster)
            //{
            //    debug_DestroyCluster = false;
            //    foreach (var enemy in currentEnemyCluster)
            //    {
            //        enemy.Value.TakeDamage(100f);
            //        enemyPoolManager.DespawnEnemy(enemy.Value);
            //        currentClusterDeadCounter++;
            //    }
            //}

            scaleEasingApplier.ManualUpdate();
            if (scaleEasingApplier.IsEasing)
            {
                var currentValue = scaleEasingApplier.currentValue;
                transform.localScale = new Vector3(currentValue, currentValue, currentValue);
            }

            HandleMovement();
            createNewCluster = currentClusterDeadCounter >= currentClusterCounter && currentEnemyCluster.Count > 0;
        }

        public EnemyClusterType CreateCluster(LevelDifficulty levelDifficulty, bool randomClusterType = false, EnemyClusterType enemyClusterType = EnemyClusterType.None)
        {
            var type = randomClusterType ? GetRandomEnemyClusterType() : enemyClusterType;
            if (type == EnemyClusterType.None)
            {
                Debug.LogError("Must have a correct enemy cluster type.");
            }

            CreateCluster(levelDifficulty, type);
            return type;
        }

        private void CreateCluster(LevelDifficulty levelDifficulty, EnemyClusterType enemyClusterType)
        {
            ClearCurrentCluster();

            transform.localScale = Vector3.zero;

            var enemyClusterFormation = enemyClusterFormationRepository.GetEnemyClusterFormationInfo(levelDifficulty, enemyClusterType);
            var formationHeight = enemyClusterFormation.height;
            var formationWidth = enemyClusterFormation.width;

            if (enemyClusterType == EnemyClusterType.Square || enemyClusterType == EnemyClusterType.Rectangle)
            {
                var tmpY = 0f;
                for (int i = 0; i < formationHeight; i++)
                {
                    var isEvenY = i % 2 == 0;
                    var changeY = isEvenY ? tmpY : -tmpY;
                    var rowPosition = new Vector3(0, changeY, 0);

                    var pooledEnemyClusterRow = enemyPoolManager.SpawnEnemyClusterRow(rowPosition, Quaternion.identity, true, transform, false);
                    currentEnemyClusterRows.Add(pooledEnemyClusterRow);

                    var tmpX = 0f;
                    for (int j = 0; j < formationWidth; j++)
                    {
                        var enemyType = GetRandomEnemyTypeBasedOnDifficulty(levelDifficulty);

                        var isEvenX = j % 2 == 0;
                        var changeX = isEvenX ? tmpX : -tmpX;
                        var enemyPosition = new Vector3(changeX, 0, 0);

                        var enemyBase = enemyPoolManager.SpawnEnemy(enemyType, enemyPosition, Quaternion.identity, true, pooledEnemyClusterRow.transform, false);

                        currentEnemyCluster.Add(currentClusterCounter, enemyBase);
                        enemyBase.enemyId = currentClusterCounter;

                        currentClusterCounter++;

                        if (isEvenX)
                        {
                            tmpX += widthSpacing;
                        }
                    }

                    if (isEvenY)
                    {
                        tmpY += heightSpacing;
                    }
                }
            }

            scaleEasingApplier.StartEase(0, 1, spawnTimeInSeconds, spawnClusterScaleEasingType);

            // TODO -> Add cluster type for daimond
        }

        // TODO -> should this be in enemy manager ???
        // returns if enemy dies
        public bool ApplyDamage(int enemyId, float damage)
        {
            if (!currentEnemyCluster.ContainsKey(enemyId))
            {
                Debug.Log("Enemy was not found in current cluster");
                return false;
            }

            var enemy = currentEnemyCluster[enemyId];
            var enemyDied = enemy.TakeDamage(damage);
            enemyPoolManager.SpawnHitFx(enemy.transform.position, Quaternion.identity);

            if (enemyDied)
            {
                enemyPoolManager.DespawnEnemy(enemy);
                currentClusterDeadCounter++;
            }

            return enemyDied;
        }

        private Vector3 proxyTargetPosition;
        private Quaternion proxyQuaternion;

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                proxyTargetPosition = (Vector3)stream.ReceiveNext();
                proxyQuaternion = (Quaternion)stream.ReceiveNext();
            }
        }

        private void HandleMovement()
        {
            if (PhotonNetwork.isMasterClient)
            {
                var targetY = Mathf.PingPong(Time.time * moveSpeed / 2, yAxisBoundary) - yAxisBoundary / 2f;
                var targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, proxyTargetPosition, 0.25f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, proxyQuaternion, 500 * Time.deltaTime);
            }
        }

        private EnemyClusterType GetRandomEnemyClusterType()
        {
            // TODO -> Range to 3 for daimond cluster type 
            var randmonNo = Random.Range(1, 3);
            return (EnemyClusterType)randmonNo;
        }

        // TODO -> Do probability function this must have a seed and be deterministic !!!
        private EnemyType GetRandomEnemyTypeBasedOnDifficulty(LevelDifficulty levelDifficulty)
        {
            return EnemyType.HeavyDroid;
        }

        public void ClearCurrentCluster()
        {
            if (currentEnemyClusterRows.Count > 0)
            {
                for (int i = 0; i < currentEnemyClusterRows.Count; i++)
                {
                    var clusterRow = currentEnemyClusterRows[i];
                    enemyPoolManager.DespawnEnemyClusterRow(clusterRow);
                }
            }

            if (currentEnemyCluster.Count > 0)
            {
                var enumerator = currentEnemyCluster.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    enemyPoolManager.DespawnEnemy(current.Value);
                }
            }

            currentEnemyClusterRows.Clear();
            currentEnemyCluster.Clear();
            currentClusterDeadCounter = 0;
            currentClusterCounter = 0;
        }
    }
}
