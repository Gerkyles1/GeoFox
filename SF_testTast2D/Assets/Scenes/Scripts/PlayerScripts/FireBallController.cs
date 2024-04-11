using System;
using UnityEngine;

namespace PlayerScripts
{
    public class FireBallController : MonoBehaviour
    {
        private int _direction;
        [SerializeField] private int _damage = 1;
        private int _speed = 10;

        private void Start()
        {
            Destroy(gameObject, 1f);
        }

        void FixedUpdate()
        {
            transform.Translate((_direction >= 0 ? Vector2.right : Vector2.left) * _speed * Time.deltaTime);
        }
        public void SetFireBallDirection(int direction)
        {
            _direction = direction >= 0 ? 1 : -1;
            transform.localScale = new Vector2(_direction * Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        public void SetFireBallDirection(int direction, int speed)
        {
            SetFireBallDirection(direction);
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
    }
}