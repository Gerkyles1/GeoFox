using EnemyScripts;
using Spells;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        public static event Action OnPlayerDied;
        public static bool _playerAlive = true;

        [SerializeField] private GameObject firePoint;

        [SerializeField] private GameObject fireBallSpellPrefab;
        [SerializeField] private GameObject lightningSpellPrefab;

        [SerializeField] private Slider hpBar;


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
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetPlayerDirection(-1);
                    _playerAnimator.SetTrigger("FireBallSpell");
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetPlayerDirection(1);
                    _playerAnimator.SetTrigger("LightningSpell");
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
            FireBallSpell fireBall = Instantiate(fireBallSpellPrefab).GetComponent<FireBallSpell>();
            fireBall.transform.position = firePoint.transform.position;
            fireBall.SetSpellDirection(_currentDirection);
        }

        private void CreateLightning()
        {
            LightningSpell lightningSpell = Instantiate(lightningSpellPrefab).GetComponent<LightningSpell>();
            lightningSpell.transform.position = firePoint.transform.position;
            lightningSpell.SetSpellDirection(_currentDirection);
        }
        private void TakeDamage(int damage)
        {
            _hp -= damage;
            hpBar.value = _hp;
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