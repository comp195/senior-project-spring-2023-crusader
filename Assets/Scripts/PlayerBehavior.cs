using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] HealthBar health_bar;
            // Start is called before the first frame update
        public GameManager gm;
        public int maxHealth = 100;

        private static int currentHealth;
        public static Transform playerTransform;
        public Animator animator;

        void Start() {
            currentHealth = maxHealth;
            playerTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                PlayerTakeDamage(20);

            }
            if(Input.GetKeyDown(KeyCode.L))
            {
                PlayerHeal(5);

            }
        }

        public void PlayerTakeDamage(int dmg)
        {
            Debug.Log(currentHealth);
            currentHealth -= dmg;
            animator.SetBool("hit", true);
            if (currentHealth <= 0) {
                Die();
            }
            health_bar.SetHealth(currentHealth);
            animator.SetBool("hit", false);
        }

        private void PlayerHeal(int healing)
        {
            GameManager1.gameManager.playerHealth.HealUnit(healing);
            health_bar.SetHealth(currentHealth);
        }

        void Die()
        {
            gm.GameOver();
        }
    }
}