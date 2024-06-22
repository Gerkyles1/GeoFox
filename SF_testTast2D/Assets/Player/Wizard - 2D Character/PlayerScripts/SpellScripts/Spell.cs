using System;
using UnityEngine;

namespace Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] public string _AnimatorTriger;
        [SerializeField] public float _lifeTime;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
        public abstract void InitialiseSpell(Vector2 position, int direction);
    }
}