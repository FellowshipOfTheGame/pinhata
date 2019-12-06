using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    private Image healthBar;
    private PlayerHealth playerHealth;

    private float MaxHealth;

    private void Awake()
    {
        healthBar = GetComponent<Image>();
        textMesh =  GetComponentInChildren<TextMeshProUGUI>();

        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.onHealthChange += HealthChange;
        MaxHealth = playerHealth.MaxHealth;
    }

    /*
     Change player health UI when player health's changes
    */
    private void HealthChange(int health)
    {
        healthBar.fillAmount = health / MaxHealth;
        textMesh.text = health + "/" + MaxHealth;
    }

    // Free delegate
    private void OnDestroy()
    {
        playerHealth.onHealthChange -= HealthChange;
    }
}
