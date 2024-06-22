using System;
using UnityEngine;

namespace Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] private string _AnimatorTriger;
        [SerializeField] private float _lifeTime;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}