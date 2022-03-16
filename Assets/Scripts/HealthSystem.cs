using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static bool destroyedObstacle, isDead;

    public Image healthBar;

    private byte maxHealth = 100;
    private float health;

    public bool testing;
    private float Health //Isso é para ter certeza de que não estamos igualando o fillAmmount a um valor negativo
    {
        get
        {
            if (health < 0)
                health = 0;
            else if (health > maxHealth)
                health = maxHealth;

            return health;
        }
    }

    private void Start()
    {
        isDead = false;
        destroyedObstacle = false;
        health = maxHealth;
        healthBar.fillAmount = 1;
    }

    private void ModifyFillAmount() //Sempre chamar caso o player tome dano
    {
        healthBar.fillAmount = ((1f / maxHealth) * Health); //Isso deixa a gnt trabalhar com números inteiros ao invé de floats entre 0 e 1
    }

    private void OnDeath()
    {
        GameManager.Singleton.AddHighestScore();
        switch (testing)
        {
            case true:
                Debug.Log("You died");
                break;
            default:
                isDead = true;
                //DeathAnimation
                Menu.Singleton.PauseGame();
                break;
        }
    }

    public void ModifyHealth(float modHealth)
    {
        health += modHealth;
        ModifyFillAmount();
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.layer == 8)
        {
            case true:
                //Bump Particles
                AudioManager.Singleton.PlaySoundEffect(2);
                StartCoroutine(CameraShake.Singleton.Shake(GameObject.FindGameObjectWithTag("MainCameraPosition").transform, 0.2f, 0.05f, 0.05f));
                GameManager.Singleton.DeleteObstacle();
                ModifyHealth(-25);
            break;
        }
    }

    private void Update()
    {
        if(!Menu.isPaused)
            ModifyHealth(-0.1f * GameManager.Singleton.velocity * Time.deltaTime);
        if(destroyedObstacle)
        {
            destroyedObstacle = false;
            GameManager.Singleton.AddScore();
            ModifyHealth(10);
        }
    }
}