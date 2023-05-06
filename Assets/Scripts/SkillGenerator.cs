using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using Player;  

public class SkillGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite speed;
    [SerializeField] Sprite hp;
    [SerializeField] Sprite dmg;
    [SerializeField] Sprite jump;

    [SerializeField] Image skillOneSprite;
    public Image skillTwoSprite;
    public Image skillThreeSprite;
    public GameObject skillOneButton;
    public GameObject skillOneText;
    public GameObject skillTwoButton;
    public GameObject skillTwoText;
    public GameObject skillThreeButton;
    public GameObject skillThreeText;
    private int[] skills = new int[3];

    [SerializeField] GameManager gm;
    public GameObject player;
    
    public void generate()
    {
        int random = (int)Random.Range(1,5);
        int random2 = (int)Random.Range(1,5);
        int random3 = (int)Random.Range(1,5);

        updateSprite(random, 1);
        updateSprite(random2, 2);
        updateSprite(random3, 3);
        skills[0] = random;
        skills[1] = random2;
        skills[2] = random3;
        Debug.Log(random + " "+ random2 + " "+ random3);
    }

    void updateSprite(int skill, int number)
    {
        switch(number)
        {
            case 1:
                switch(skill)
                {
                    case 1:
                        skillOneSprite.sprite = speed;
                        skillOneText.GetComponent<Text>().text = "Speed Up";
                        break;
                    case 2:
                        skillOneSprite.sprite = hp;
                        skillOneText.GetComponent<Text>().text= "HP Up";
                        break;
                    case 3:
                        skillOneSprite.sprite = dmg;
                        skillOneText.GetComponent<Text>().text = "Damage Up";
                        break;
                    case 4:
                        skillOneSprite.sprite = jump;
                        skillOneText.GetComponent<Text>().text = "Jump Increase";
                        break;
                } 
                break;
            case 2:
                switch(skill)
                {
                    case 1:
                        skillTwoSprite.sprite = speed;
                        skillTwoText.GetComponent<Text>().text = "Speed Up";
                        break;
                    case 2:
                        skillTwoSprite.sprite = hp;
                        skillTwoText.GetComponent<Text>().text= "HP Up";
                        break;
                    case 3:
                        skillTwoSprite.sprite = dmg;
                        skillTwoText.GetComponent<Text>().text = "Damage Up";
                        break;
                    case 4:
                        skillTwoSprite.sprite = jump;
                        skillTwoText.GetComponent<Text>().text = "Jump Increase";
                        break;
                } 
                break;
            case 3:
                switch(skill)
                {
                    case 1:
                        skillThreeSprite.sprite = speed;
                        skillThreeText.GetComponent<Text>().text = "Speed Up";
                        break;
                    case 2:
                        skillThreeSprite.sprite = hp;
                        skillThreeText.GetComponent<Text>().text= "HP Up";
                        break;
                    case 3:
                        skillThreeSprite.sprite = dmg;
                        skillThreeText.GetComponent<Text>().text = "Damage Up";
                        break;
                    case 4:
                        skillThreeSprite.sprite = jump;
                        skillThreeText.GetComponent<Text>().text = "Jump Increase";
                        break;
                } 
                break;
        }
    }

    public void skillObtain()
    {
        switch(skills[0])
        {
            case 1:
                player.GetComponent<PlayerMovement>().moveSpeed *= 1.5f;
                break;
            case 2:
                player.GetComponent<PlayerBehavior>().maxHealth += 100;
                player.GetComponent<PlayerBehavior>().PlayerHeal(100);
                break;
            case 3:
                player.GetComponent<Player_Combat>().damage *= 2;
                break;
            case 4:
                player.GetComponent<PlayerMovement>().jumpForce += 10;
                break;
        }
        gm.SkillUnpause();
    }

    public void skillObtain2()
    {
        /*switch(skills[1])
        {
            case 1:
            case 2:
            case 3:
            case 4:
        }*/
        gm.SkillUnpause();
    }
    public void skillObtain3()
    {
        /*switch(skills[2])
        {
            case 1:
            case 2:
            case 3:
            case 4:
        }*/
        gm.SkillUnpause();
    }
}
