using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MeinMenuScripts
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private Scene scene;
        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => SceneManager.LoadScene(scene.name));
        }
    }
}