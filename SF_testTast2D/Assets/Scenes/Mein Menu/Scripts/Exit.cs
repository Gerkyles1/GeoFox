using UnityEngine;
using UnityEngine.UI;

namespace MeinMenuScripts
{
    public class Exit : MonoBehaviour
    {
        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => Application.Quit());
        }
    }
}