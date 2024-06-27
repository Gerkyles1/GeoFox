using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MeinMenuScripts
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string scene;
        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(scene);
            });
        }
    }
}