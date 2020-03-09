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

    /**
     * Setup player health
     **/
    public void Setup(GameObject player)
    {
        healthBar = GetComponent<Image>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        playerHealth = player.GetComponent<PlayerHealth>();
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
