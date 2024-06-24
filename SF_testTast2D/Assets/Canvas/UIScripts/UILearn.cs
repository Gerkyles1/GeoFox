using MeinMenuScripts;
using UnityEngine;
using UnityEngine.UI;

public class UILearn : MonoBehaviour
{
    [SerializeField] private GameObject[] _learnSlides;
    [SerializeField] private int _levelForLearn;
    private GameObject _learnParent;
    private int _currentIndex = 1; 

    void Start()
    {
        _learnParent = transform.parent.gameObject;

        if (!(StartGame._choosedLevel == _levelForLearn))
        {
            if(_learnParent.transform.childCount == 1)
                Destroy(_learnParent);


            Debug.Log(StartGame._choosedLevel);
            Destroy(gameObject);
        }
        GetComponent<Button>().onClick.AddListener(ActivateNext);
    }


    public void ActivateNext()
    {
        if (_learnSlides == null || _learnSlides.Length == 0 || _currentIndex >=_learnSlides.Length)
        {
            Destroy(_learnParent);
            return;
        }

        _learnSlides[_currentIndex-1].SetActive(false);
        _learnSlides[_currentIndex].SetActive(true);
        _currentIndex++;
    }


}
