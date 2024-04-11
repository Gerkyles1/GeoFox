using UIScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class SpawnPoint : MonoBehaviour
    {
        private static GameObject[] _enemyPrefabs;
        private static int _enemyCount = 0;
        private static int _maxEnemyCount = 3;

        private float lastSpawnTime;
        private float spawnCooldown = 1.5f;

        private void Start()
        {
            if(_enemyPrefabs == null)
                _enemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy");

            EnemyController.OnEnemyDied += MinusEnemy;
            UIController.SceneReload += SceneReload;
            SpawnRandomEnemy();
        }

        private void MinusEnemy()
        {
            _enemyCount--;
            
            _maxEnemyCount = _maxEnemyCount<15?_maxEnemyCount+1:1;
            SpawnRandomEnemy();
        }

        private void SpawnRandomEnemy()
        {
            if (Time.time - lastSpawnTime >= spawnCooldown || _enemyCount > 5)
            {
                GameObject newEnemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], transform);
                newEnemy.transform.SetParent(null);
                _enemyCount++;
                lastSpawnTime = Time.time;
            }
        }

        private void SpawnEnoughEnemy()
        {
            while (_enemyCount < _maxEnemyCount)
            {
                if(Random.Range(0f, 1f)>0.5f)
                    SpawnRandomEnemy();
            }
        }
        private void SceneReload()
        {
            _enemyCount = 0;
            _enemyPrefabs = null;
            _maxEnemyCount = 1;
        }

        private void OnDestroy()
        {
            EnemyController.OnEnemyDied -= MinusEnemy;
            UIController.SceneReload -= SceneReload;

        }
    }
}