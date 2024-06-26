using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    [System.Serializable]
    public class Level
    {
        public List<Wave> waves;
    }


    [System.Serializable]
    public class Wave
    {
        public List<EnemyType> enemies;
    }


    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public int count;
        public float spawnRate;
        public float waitAfterMiniWave;
    }


    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels;
        private List<Wave> _waves;
        public bool _waveEnd { get; private set; } = false;

        private int _currentWaveIndex = 0;
        private void Start()
        {
            _waves = _levels[MeinMenuScripts.StartGame._choosedLevel - 1].waves;


            WaveController.OnStartNewWave += NewWave;

        }

        public bool LevelEnd()
        {
            return _currentWaveIndex >= _waves.Count;
        }


        public void NewWave()
        {
            _waveEnd = false;
            StartCoroutine(CallWave());
        }


        public IEnumerator CallWave()
        {
            if (_currentWaveIndex >= _waves.Count)
            {
                _waveEnd = true;
                yield break;
            }

            foreach (EnemyType enemy in _waves[_currentWaveIndex].enemies)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    Instantiate(enemy.enemyPrefab, transform);
                    WaveController._enemyCount++;
                    yield return new WaitForSeconds(enemy.spawnRate);


                }
                yield return new WaitForSeconds(enemy.waitAfterMiniWave);

            }
            _waveEnd = true;
            _currentWaveIndex++;
        }
        private void OnDestroy()
        {
            WaveController.OnStartNewWave -= NewWave;
        }
    }
}
