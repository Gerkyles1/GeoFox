using UnityEngine.SceneManagement;
using UnityEngine;

namespace MeinMenuScripts
{
    public class StartGame : MonoBehaviour
    {
        public static int _choosedLevel { get; private set; } = 1;
        private int _locationNumber = 1;
        public void StartLevel(int level)
        {
            _choosedLevel = level;
            SceneManager.LoadScene(_locationNumber.ToString());
        }
    }
}