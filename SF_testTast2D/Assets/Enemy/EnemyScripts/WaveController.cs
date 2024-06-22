using System;
using UnityEngine;

namespace EnemyScripts
{
    public class WaveController : MonoBehaviour
    {
        public static event Action OnStartNewWave;

        public static event Action OnEndNewWave;
        [SerializeField] private GameObject _newWaveButton;


        static public int _enemyCount = 0;


        [SerializeField] private EnemySpawner[] _spawners;

        private void Start()
        {
            EnemyController.OnEnemyDied += DecEnemyCount;
            InvokeRepeating("CheckEndWave", 2f, 2f);
        }

        public void StartWave()
        {
            _newWaveButton.SetActive(false);
            OnStartNewWave?.Invoke();
        }
        private void DecEnemyCount()
        {
            _enemyCount--;
            CheckEndWave();
        }

        private void OnDestroy()
        {
            EnemyController.OnEnemyDied -= DecEnemyCount;
        }
        private void CheckEndWave()
        {
            if (_enemyCount == 0)
            {
                foreach (var spawner in _spawners)
                    if (!spawner._waveEnd)
                        return;

                _newWaveButton.SetActive(true);
            }
        }
    }
}