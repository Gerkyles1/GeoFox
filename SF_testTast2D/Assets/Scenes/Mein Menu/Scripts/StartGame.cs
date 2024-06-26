using UnityEngine.SceneManagement;
using UnityEngine;

namespace MeinMenuScripts
{
    public class StartGame : MonoBehaviour
    {
        public static int _choosedLevel { get; private set; }
        private static int _locationNumber = 1;
        public static void StartLevel(int level)
        {
            _choosedLevel = level;
            Debug.Log("Start Level " + _choosedLevel);
            SceneManager.LoadScene(_locationNumber.ToString());
        }
        public static void StartNextLevel()
        {
            if (_choosedLevel == 0)
                StartLevel(SavesController.curentMaxLevel);
            else
                StartLevel(_choosedLevel+1);
        }
    }
}