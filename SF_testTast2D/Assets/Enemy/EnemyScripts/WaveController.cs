using System;
using UIScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class WaveController : MonoBehaviour
    {
        public static event Action OnStartNewWave;

        [SerializeField] private GameObject _newWaveButton;
        [SerializeField] private EnemySpawner[] _spawners;
        public static event Action OnPlayerWin;


        static public int _enemyCount = 0;
        private void ResetEnemyCount() => _enemyCount = 0;



        private void Start()
        {
            EnemyController.OnEnemyDied += DecEnemyCount;
            UIController.SceneReload += ResetEnemyCount;
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

        private void CheckEndWave()
        {

            if (_enemyCount == 0)
            {

                foreach (var spawner in _spawners)
                    if (!spawner._waveEnd)
                        return;

                foreach (var spawner in _spawners)
                    if (!spawner.LevelEnd())
                    {
                        _newWaveButton.SetActive(true);
                        return;
                    }

                OnPlayerWin?.Invoke();




            }
        }
        private void OnDestroy()
        {
            EnemyController.OnEnemyDied -= DecEnemyCount;
            UIController.SceneReload -= ResetEnemyCount;

        }
    }
}