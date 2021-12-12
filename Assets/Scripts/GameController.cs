using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera cam;
    private float maxWidth;

    public GameObject ball;
    public GameObject bomb;

    public float timeLeft;

    public float bombFrequency = 0.2f;
    public Text timerText;

    public int STATE_DRIVE = 0;
    public int STATE_GAMEOVER = 1; 
    public int gameState;

    void Start()
    {
        gameState = STATE_DRIVE;

        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
	
        float ballWidth = ball.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;

        StartCoroutine(Spawn());
    }

    private void UpdateText()
    {
        timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
    }

    private void FixedUpdate()
    {
        if (gameState == STATE_GAMEOVER)
        {
            return;
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        UpdateText();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);

        while (timeLeft > 0 && gameState == STATE_DRIVE)
        {
            Vector3 spawnPosition =
                new Vector3(Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    0.0f);

            Quaternion spawnRotation = Quaternion.identity;

            float rand = Random.Range(0f, 1f);
            if(rand > bombFrequency)
            {
                Instantiate (ball, spawnPosition, spawnRotation);
            }
            else
            {
                Instantiate (bomb, spawnPosition, spawnRotation);
            }
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }
}