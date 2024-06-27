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
        public void Reset()
        {
            _AnimatorTriger = "FireBallSpell";
            _lifeTime = 1;
            _level = 1;
            _costScale = 5;
            _nameStatToUpgrade = "Damage";
            _level = 1;
            _damage = 2;
            _speed = 10;
        }

        void FixedUpdate()
        {
            transform.Translate((_direction >= 0 ? Vector2.right : Vector2.left) * _speed * Time.deltaTime);
        }
        public override void InitialiseSpell(Vector2 position, int direction)
        {
            transform.position = position;
            SetSpellDirection(direction);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().Damage(_damage);

                GameObject vfx = Instantiate(_fireBallVfx);
                vfx.transform.position = collision.ClosestPoint(transform.position);

                Destroy(vfx, 1f);

                if (_level < 5)
                    Destroy(gameObject);
            }
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

        public override void GetStats(ref string oldStat, ref string newStat)
        {
            oldStat = _damage.ToString();
            newStat = ((_level + 1) * 2).ToString();

        }

        public override void UpgrateSpell()
        {

            _level++;
            _damage = _level * 2;
            return;
        }
    }
}