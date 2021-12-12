using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    public Camera cam;
    public GameController game;
    private float maxWidth;
    public float offsetY = 0;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);

        float hatWidth = GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - hatWidth;
    }

    void FixedUpdate()
    {
        if (game.gameState == game.STATE_GAMEOVER)
        {
            return;
        }
        Vector3 rawposition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(rawposition.x, offsetY, 0.0f);

        float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
        targetPosition = new Vector3(targetWidth, targetPosition.y + offsetY, targetPosition.z);
        GetComponent<Rigidbody2D>().MovePosition(targetPosition);
    }
}
