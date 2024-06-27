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
        [SerializeField] private Slider hpBar;
        [SerializeField] private string _activeSpellAnimatorTriger = "FireBallSpell";
        [SerializeField] private Button _leftAttack;
        [SerializeField] private Button _rightAttack;
        public void SetActiveSpellAnimatorTriger(string triger) => _activeSpellAnimatorTriger = triger;




        private Animator _playerAnimator;
        private int _currentDirection = 1;
        private int _hp = 10;

        private void Start()
        {
            _playerAlive = true;
            _playerAnimator = GetComponent<Animator>();
            EnemyController.EnemyAttackPlayer += TakeDamage;
            _leftAttack.onClick.AddListener(LeftAttack);
            _rightAttack.onClick.AddListener(RightAttack);


        }
        private void Update()
        {
            if (_playerAlive)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    LeftAttack();
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RightAttack();
                }

            }
        }

        private void LeftAttack()
        {
            SetPlayerDirection(-1);
            _playerAnimator.SetTrigger(_activeSpellAnimatorTriger);
        }

        private void RightAttack()
        {
            SetPlayerDirection(1);
            _playerAnimator.SetTrigger(_activeSpellAnimatorTriger);
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

        private void CreateSpell(GameObject SpellPrefab)
        {
            Spell spell = Instantiate(SpellPrefab).GetComponent<Spell>();
            spell.InitialiseSpell(firePoint.transform.position, _currentDirection);
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