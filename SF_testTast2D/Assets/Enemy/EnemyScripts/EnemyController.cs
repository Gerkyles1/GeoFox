using MeinMenuScripts;
using PlayerScripts;
using Spells;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyScripts
{
    public class EnemyController : MonoBehaviour
    {

        public static event Action OnEnemyDied;
        public static event Action<int> EnemyAttackPlayer;

        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _hp = 3;
        [SerializeField] private int _coinsForKill = 1;
        [SerializeField] private Slider _hpBar;
        [SerializeField] private GameObject _ground;

        private Transform _targetPoint;
        private Animator _animator;
        private bool _enemyAlive = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.speed = _moveSpeed;

            if (_targetPoint == null)
                _targetPoint = GameObject.FindGameObjectWithTag("Player").transform;

            transform.localScale = new Vector2((transform.position.x < _targetPoint.position.x ? 1 : -1) * transform.localScale.x, transform.localScale.y);

            _hpBar.GetComponentInParent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            _hpBar.maxValue = _hp;
            _hpBar.value = _hp;
            _hpBar.enabled = false;
            _hpBar.direction = Slider.Direction.RightToLeft;
        }
        void FixedUpdate()
        {
            if (PlayerController._playerAlive && _enemyAlive)
            {
                if (Vector2.Distance(transform.position, _targetPoint.position) > _attackRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _targetPoint.position, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    _animator.SetTrigger("Attack");
                }
            }
        }

        private void AttackPlayer()
        {
            EnemyAttackPlayer?.Invoke(_damage);
        }


        public void Damage(int damage)
        {
            _hp -= damage;
            _hpBar.value = _hp;
            if (_hp <= 0)
            {
                SavesController.coinsCount += _coinsForKill;
                OnEnemyDied?.Invoke();
                _enemyAlive = false;
                _animator.SetTrigger("Die");
                GetComponent<Collider2D>().enabled = false;
                GetComponentInChildren<Collider2D>().enabled = false;
            }
        }


        private void Die()
        {
            Destroy(gameObject);
        }
    }
}