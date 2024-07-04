using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleted : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Game completed!");
        Application.Quit();
    }
}
