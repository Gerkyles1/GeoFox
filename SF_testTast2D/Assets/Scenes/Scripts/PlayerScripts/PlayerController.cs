using EnemyScripts;
using System;
using UIScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        public static bool _playerAlive = true;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private GameObject fireBallPrefab;
        public static event Action OnPlayerDied;


        private Animator _playerAnimator;
        private int _currentDirection = 1;
        private int _hp = 10;

        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
            EnemyController.EnemyAttackPlayer += TakeDamage;
        }
        private void Update()
        {
            if (_playerAlive)
            {
                Input.GetKeyDown(KeyCode.A);
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetPlayerDirection(-1);
                    _playerAnimator.SetTrigger("Attack");
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetPlayerDirection(1);
                    _playerAnimator.SetTrigger("Attack");
                }
            }
        }

        public void SetPlayerDirection(int direction)
        {


            if (direction > 0)
            {
                _currentDirection = 1;
                transform.localScale = new Vector2(_currentDirection * Math.Abs(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                _currentDirection = -1;
                transform.localScale = new Vector2(_currentDirection * Math.Abs(transform.localScale.x), transform.localScale.y);
            }
        }

        private void CreateFireBall()
        {
            FireBallController fireBall = Instantiate(fireBallPrefab).GetComponent<FireBallController>();
            fireBall.transform.position = firePoint.transform.position;
            fireBall.SetFireBallDirection(_currentDirection);
        }
        private void TakeDamage(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                OnPlayerDied?.Invoke();
                Destroy(gameObject, 2f);
                _playerAlive = false;
                _playerAnimator.SetTrigger("Die");
            }
        }
        private void OnDestroy()
        {
            EnemyController.EnemyAttackPlayer -= TakeDamage;

        }
    }
}