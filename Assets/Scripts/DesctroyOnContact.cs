using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesctroyOnContact : MonoBehaviour
{
    public GameController game;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            game.gameState = game.STATE_GAMEOVER;
        }
        else{
            Destroy(other.gameObject);
        }
    }
}
