using System;
using UnityEngine;

namespace Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField]public string _AnimatorTriger;
        [SerializeField]public float _lifeTime;
        [SerializeField] public int _level;
        [SerializeField] public float _costScale;
        [SerializeField] public string _nameStatToUpgrade;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
        public abstract void InitialiseSpell(Vector2 position, int direction);
        public abstract void GetStats(ref string oldStat, ref string newStat);
    }
}