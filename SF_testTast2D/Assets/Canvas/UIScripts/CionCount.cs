using MeinMenuScripts;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UIScripts
{
    public class CionCount : MonoBehaviour
    {
        private TextMeshProUGUI _counter;
        private void Start()
        {
            _counter = GetComponent<TextMeshProUGUI>();
            _counter.text = SavesController.coinsCount.ToString();
            SavesController.OnCoinsCountChange += UpdateCoinCount;
        }
        private void UpdateCoinCount() => _counter.text = SavesController.coinsCount.ToString();

        private void OnDestroy()
        {
            SavesController.OnCoinsCountChange -= UpdateCoinCount;
        }


    }
}