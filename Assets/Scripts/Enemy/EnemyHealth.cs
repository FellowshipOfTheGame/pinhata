using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private int MaxHealth = 10;
    [SerializeField]
    private GameObject ItemPrefab = null;
    [SerializeField]
    [Range(0, 1)]
    private float DropItemRate = 1;

    private EnemyMovement movement;

    private int health;

    public bool invincible;

    private void Awake() {
        health = MaxHealth;
        movement = GetComponent<EnemyMovement>();
        invincible = true;
    }

    public bool Alive() {
        return health > 0;
    }

    private void Die() {
        EnemyManager.Instance.EnemyDied();
        GetComponentInChildren<MariachiAnimHandle>().Die();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void Drop() {
        float random = Random.Range(0, 100) / 100f;
        if (random <= DropItemRate) {
            var position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(ItemPrefab, position, ItemPrefab.transform.rotation);
        }
    }

    public void TakeDamage(int damage) {
        if(!invincible){
            health -= damage;

            if (!Alive()) {
                Drop();
                Die();
            }else{
                movement.PauseMovement(1f);
                GetComponentInChildren<MariachiAnimHandle>().TakeDamage();
            }
        }
    }
}
