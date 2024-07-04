using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingLives : MonoBehaviour
{
    static string livesKey = "lives";
    public static int remainingLives { get; set; }


    private void Awake()
    {
        remainingLives = PlayerPrefs.GetInt(livesKey, 3);
        Debug.Log("AWAKE. Remaining lives: " + GetLives());
    }

    public static int GetLives()
    {
        return remainingLives;
    }

    public static void SetLives(int lives)
    {
        PlayerPrefs.SetInt(livesKey, remainingLives);
        Debug.Log("SET LIVES. Remaining lives: " + GetLives());
    }
}
