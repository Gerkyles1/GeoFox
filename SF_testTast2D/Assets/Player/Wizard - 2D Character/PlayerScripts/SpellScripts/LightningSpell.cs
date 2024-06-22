using EnemyScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    public class LightningSpell : Spell
    {
        private int _direction;
        private bool _needToMove = true;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _speed = 10;
        [SerializeField] private GameObject _effects;



        void FixedUpdate()
        {
            if (_needToMove)
                transform.Translate((_direction >= 0 ? Vector2.right : Vector2.left) * _speed * Time.deltaTime);
        }
        public void SetSpellDirection(int direction)
        {
            _direction = direction >= 0 ? 1 : -1;
            transform.localScale = new Vector2(_direction * Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        public void SetDamage(int damage)
        {
            if (damage != 0)
                this._damage = Mathf.Abs(damage);
            else
                this._damage = 1;
        }
        public int GetDamage()
        {
            return _damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                _needToMove = false;
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);


                GameObject vfx = Instantiate(_effects);
                vfx.transform.position = collision.transform.position;
                Destroy(vfx, 1f);

                collision.GetComponent<EnemyController>().Damage(_damage);

                Destroy(gameObject);
            }
        }
        public override void InitialiseSpell(Vector2 position, int direction)
        {
            transform.position = position;
            SetSpellDirection(direction);
        }

    }
}