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
            Debug.Log("initbutton " + Convert.ToInt16(_childrenTMP.text));
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GetComponentInParent<StartGame>().StartLevel(Convert.ToInt16(_childrenTMP.text));
            });
        }
    }
}