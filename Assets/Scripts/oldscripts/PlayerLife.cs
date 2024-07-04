using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource enemyHit;
    private int lives;

    [SerializeField] private AudioSource deathSoundEffect;

    [SerializeField] private GameObject[] lifeIcon;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lives = RemainingLives.GetLives();

        if (RemainingLives.GetLives() == 3)
        {
            Debug.Log("3 lives");
            lifeIcon[0].SetActive(true);
            lifeIcon[1].SetActive(true);
            lifeIcon[2].SetActive(true);
        }
        else if (RemainingLives.GetLives() == 2)
        {
            Debug.Log("2 lives");
            lifeIcon[0].SetActive(false);
            lifeIcon[1].SetActive(true);
            lifeIcon[2].SetActive(true);
        }
        else if (RemainingLives.GetLives() == 1)
        {
            Debug.Log("1 life");
            lifeIcon[0].SetActive(false);
            lifeIcon[1].SetActive(false);
            lifeIcon[2].SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int remainingLives = RemainingLives.GetLives();
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            RemainingLives.SetLives(remainingLives - 1);
            Debug.Log("PLAYERLIFE. Remaining lives: " + RemainingLives.GetLives());
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Animator enemyAnim = collision.gameObject.GetComponent<Animator>();
            // check if the player is above the enemy
            float heightDifference = transform.position.y - collision.transform.position.y;
            if (heightDifference > 0.5f)
            {
                EnemyDeathAnimation(enemyAnim);
                Destroy(collision.gameObject, .5f);
                rb.velocity = new Vector2(rb.velocity.x, 14f);
            }
            else
            {
                Die();
                RemainingLives.SetLives(remainingLives - 1);
                Debug.Log("PLAYERLIFE. Remaining lives: " + RemainingLives.GetLives());
            }
        }
    }

    private void EnemyDeathAnimation(Animator enemyAnim)
    {
        // stop enemy in place
        enemyAnim.gameObject.GetComponent<EnemyAI>().enabled = false;

        //disable enemy collider
        enemyAnim.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // play death sound
        enemyHit = enemyAnim.gameObject.GetComponent<AudioSource>();
        enemyHit.Play();

        if (enemyAnim.gameObject.name.Contains("Mushroom"))
        {
            enemyAnim.Play("Mushroom_hit");
        }
        else if (enemyAnim.gameObject.name.Contains("Snail"))
        {
            enemyAnim.Play("Snail_death");
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        // SceneManager.LoadScene(0);
        if (lifeIcon.Length == 3)
        {
            Destroy(lifeIcon[0]);
        }
        else if (lifeIcon.Length == 2)
        {
            Destroy(lifeIcon[1]);
        }
        else if (lifeIcon.Length == 1)
        {
            Destroy(lifeIcon[2]);
        }
    }

    private void RestartLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {

    }
}
