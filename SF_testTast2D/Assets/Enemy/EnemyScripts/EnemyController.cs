using PlayerScripts;
using Spells;
using System;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyController : MonoBehaviour
    {
        public static event Action OnEnemyDied;
        public static event Action<int> EnemyAttackPlayer;

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private int damage = 1;
        [SerializeField] private int hp = 3;

        private Transform _targetPoint;
        private Animator _animator;
        private bool _enemyAlive = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.speed = moveSpeed;

            if (_targetPoint == null)
                _targetPoint = GameObject.FindGameObjectWithTag("Player").transform;




                transform.localScale = new Vector2((transform.position.x < _targetPoint.position.x ? 1 : -1) * transform.localScale.x, transform.localScale.y);
        }
        void FixedUpdate()
        {
            if (PlayerController._playerAlive && _enemyAlive)
            {
                if (Vector2.Distance(transform.position, _targetPoint.position) > attackRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _targetPoint.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    _animator.SetTrigger("Attack");
                }
            }
        }

        private void AttackPlayer()
        {
            EnemyAttackPlayer?.Invoke(damage);
        }


        public void Damage(int damage)
        {
                hp -= damage;
                if (hp <= 0)
                {
                    OnEnemyDied?.Invoke();
                    _enemyAlive = false;
                    _animator.SetTrigger("Die");
                    GetComponent<Collider2D>().enabled = false;
                }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}