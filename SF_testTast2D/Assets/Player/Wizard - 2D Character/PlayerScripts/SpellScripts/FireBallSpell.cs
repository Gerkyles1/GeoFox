using EnemyScripts;
using System;
using UnityEngine;

namespace Spells
{
    public class FireBallSpell : Spell
    {
        private int _direction;
        [SerializeField] private int _damage = 1;
        [SerializeField] private GameObject _fireBallVfx;
        [SerializeField] private int _speed = 10;

        void FixedUpdate()
        {
            transform.Translate((_direction >= 0 ? Vector2.right : Vector2.left) * _speed * Time.deltaTime);
        }
        public void SetSpellDirection(int direction)
        {
            _direction = direction >= 0 ? 1 : -1;
            transform.localScale = new Vector2(_direction * Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        public void SetFireBallDirection(int direction, int speed)
        {
            SetSpellDirection(direction);
            _speed = speed > 0 ? speed : 10;
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
                collision.GetComponent<EnemyController>().Damage(_damage);

                GameObject vfx = Instantiate(_fireBallVfx);
                vfx.transform.position = collision.ClosestPoint(transform.position);

                Destroy(vfx, 1f);
                Destroy(gameObject);
            }
        }
    }
}