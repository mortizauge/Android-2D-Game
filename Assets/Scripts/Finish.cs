using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private AudioSource bgMusic;
    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        bgMusic = GameObject.Find("BG Music").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !levelCompleted)
        {
            // disable player movement
            collision.GetComponent<PlayerMovement>().enabled = false;
            levelCompleted = true;
            bgMusic.Stop();
            finishSound.Play();
            Invoke("CompleteLevel", 1.5f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
