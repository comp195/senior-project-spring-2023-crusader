using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] HealthBar health_bar;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerTakeDamage(20);
            Debug.Log(GameManager1.gameManager.playerHealth.Health);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            PlayerHeal(5);
            Debug.Log(GameManager1.gameManager.playerHealth.Health);
        }
    }

    private void PlayerTakeDamage(int dmg)
    {
        GameManager1.gameManager.playerHealth.DamageUnit(dmg);
        Debug.Log(GameManager1.gameManager.playerHealth.Health);
        health_bar.SetHealth(GameManager1.gameManager.playerHealth.Health);
    }

    private void PlayerHeal(int healing)
    {
        GameManager1.gameManager.playerHealth.HealUnit(healing);
        health_bar.SetHealth(GameManager1.gameManager.playerHealth.Health);
    }
}
