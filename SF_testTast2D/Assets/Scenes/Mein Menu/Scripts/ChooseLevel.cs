using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MeinMenuScripts
{
    public class ChooseLevel : MonoBehaviour
    {
        private void Start()
        {
            TextMeshProUGUI _childrenTMP = GetComponentInChildren<TextMeshProUGUI>();
            _childrenTMP.text = gameObject.name;

            GetComponent<Button>().interactable = Convert.ToInt16(_childrenTMP.text) <= SavesController.curentMaxLevel;

            GetComponent<Button>().onClick.AddListener(() =>
            {
                StartGame.StartLevel(Convert.ToInt16(_childrenTMP.text));
            });
            SavesController.OnMaxLevelChange += UpdeteLevelButton;
        }


        public void UpdeteLevelButton()
        {
            GetComponent<Button>().interactable = Convert.ToInt16(gameObject.name) <= SavesController.curentMaxLevel;
        }
        private void OnDestroy()
        {
            SavesController.OnMaxLevelChange -= UpdeteLevelButton;
        }
    }
}