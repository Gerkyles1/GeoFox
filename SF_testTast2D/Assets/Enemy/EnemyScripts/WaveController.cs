using System;
using UnityEngine;

namespace EnemyScripts
{
    public class WaveController : MonoBehaviour
    {
        public static event Action OnStartNewWave;

        public static event Action OnEndNewWave;

        public void StartWave()
        {
            OnStartNewWave?.Invoke();
        }
    }
}