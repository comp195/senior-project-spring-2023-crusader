using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{

    public static GameManager1 gameManager { get; private set; }

    public UnitHealth playerHealth = new UnitHealth(1000,1000);

    // Start is called before the first frame update
    void Awake()
    {
        if(gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
}
