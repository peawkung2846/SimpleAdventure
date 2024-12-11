using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;
    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found. Make sure it has tag 'Player'");
        }

        playerDamageable = player.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    public void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    public void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    public void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
