using System;
using System.Collections;
using System.Collections.Generic;
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
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GetComponentInParent<StartGame>().StartLevel(Convert.ToInt16(_childrenTMP));
            });
        }
    }
}