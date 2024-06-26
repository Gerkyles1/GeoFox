using Spells;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MeinMenuScripts
{
    public class ShopSpellButton : MonoBehaviour
    {
        [SerializeField] private Spell _spellPrefab;
        [SerializeField] private Image _spellImage;
        [SerializeField] private TextMeshProUGUI _lvlOld;
        [SerializeField] private TextMeshProUGUI _lvlNew;
        [SerializeField] private TextMeshProUGUI _statName;
        [SerializeField] private TextMeshProUGUI _statOld;
        [SerializeField] private TextMeshProUGUI _statNew;
        [SerializeField] private GameObject _upgrateButton;
        private GameObject _lockImage;


        private void Start()
        {
            _lockImage = transform.Find("lock").gameObject;
            _lockImage.SetActive(_spellPrefab._level == 0);

            GetComponent<Button>().onClick.AddListener(() =>
            {
                showUpgrateSpellData();
                _upgrateButton.GetComponent<Button>().onClick.RemoveAllListeners();
                _upgrateButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (SavesController.coinsCount >= ((_spellPrefab._level + 1) * _spellPrefab._costScale))
                    {
                        SavesController.coinsCount -= (int)((_spellPrefab._level + 1) * _spellPrefab._costScale);
                        _spellPrefab.UpgrateSpell();
                        showUpgrateSpellData();
                    }
                });

            });



        }

        private void showUpgrateSpellData()
        {
            _spellImage.sprite = transform.Find("spell").GetComponent<Image>().sprite;

            _lvlOld.text = _spellPrefab._level.ToString();
            _lvlNew.text = (_spellPrefab._level + 1).ToString();

            _statName.text = _spellPrefab._nameStatToUpgrade;
            string old = "", New = ""; _spellPrefab.GetStats(ref old, ref New);
            _statOld.text = old;
            _statNew.text = New;

            _upgrateButton.GetComponentInChildren<TextMeshProUGUI>().text = ((_spellPrefab._level + 1) * _spellPrefab._costScale).ToString();

        }


    }
}