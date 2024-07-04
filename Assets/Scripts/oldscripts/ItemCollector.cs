using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int coins = 0;

    [SerializeField] private Text coinsText;
    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource coinSoundEffect;
    [SerializeField] private AudioSource cherrySoundEffect;
    [SerializeField] private AudioSource lifeSoundEffect;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinSoundEffect.Play();
            CoinAnimation(collision);
            Destroy(collision.gameObject, .5f);
            coins++;
            coinsText.text = "Coins: " + coins;
        }
        else if (collision.gameObject.CompareTag("Cherry"))
        {
            cherrySoundEffect.Play();
            CoinAnimation(collision);
            Destroy(collision.gameObject, .5f);
            cherriesText.text = "Secret cherry found!";
            cherriesText.GetComponent<Animator>().Play("FadeOut");
        }
        else if (collision.gameObject.CompareTag("Life"))
        {
            lifeSoundEffect.Play();
            CoinAnimation(collision);
            Destroy(collision.gameObject, .5f);   
        }
    }

    private void CoinAnimation(Collider2D collision)
    {
        collision.gameObject.GetComponent<Animator>().Play("Coin_collected");
    }
}
