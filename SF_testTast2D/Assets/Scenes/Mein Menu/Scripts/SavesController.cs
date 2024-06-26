using System;
using UnityEngine;

namespace MeinMenuScripts
{
    public class SavesController : MonoBehaviour
    {
        private static int _coinsCount;
        public static int coinsCount
        {
            get { return PlayerPrefs.GetInt(COINS_COUNT_KEY); }
            set
            {
                if (value < 0)
                    PlayerPrefs.SetInt(COINS_COUNT_KEY, 0);
                else
                    PlayerPrefs.SetInt(COINS_COUNT_KEY, value);

                OnCoinsCountChange?.Invoke();
            }
        }
        private static readonly string COINS_COUNT_KEY = "COINS_COUNT";
        public static event Action OnCoinsCountChange;



        private static int _curentMaxLevel;
        public static int curentMaxLevel
        {
            get { return _curentMaxLevel = PlayerPrefs.GetInt(MAX_LEVEL_KEY); }
            set
            {
                if (value < 1)
                    PlayerPrefs.SetInt(MAX_LEVEL_KEY, 1);
                else
                    PlayerPrefs.SetInt(MAX_LEVEL_KEY, value); 

                OnMaxLevelChange?.Invoke();
            }
        }
        private static readonly string MAX_LEVEL_KEY = "MAX_LEVEL";
        public static event Action OnMaxLevelChange;


        void Start()
        {
            if (!PlayerPrefs.HasKey(COINS_COUNT_KEY))
            {
                PlayerPrefs.SetInt(COINS_COUNT_KEY, 0);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey(MAX_LEVEL_KEY))
            {
                PlayerPrefs.SetInt(MAX_LEVEL_KEY, 1);
                PlayerPrefs.Save();
            }
            _coinsCount = PlayerPrefs.GetInt(COINS_COUNT_KEY);
            OnCoinsCountChange?.Invoke();

            _curentMaxLevel = PlayerPrefs.GetInt(MAX_LEVEL_KEY);
            OnMaxLevelChange?.Invoke();
        }
        public void ResetSavedData()
        {
            PlayerPrefs.SetInt(COINS_COUNT_KEY, 0);
            PlayerPrefs.SetInt(MAX_LEVEL_KEY, 1);
            Debug.Log("reset");
            PlayerPrefs.Save();
        }
    }
}