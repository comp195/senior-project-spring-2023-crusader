using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider health_slider;

    private void Start()
    {
        health_slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int max_health)
    {
        health_slider.maxValue = max_health;
        health_slider.value = max_health;
    }

    public void SetHealth(int health)
    {
        health_slider.value = health;
    }
}
