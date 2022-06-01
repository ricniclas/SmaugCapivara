using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private BoxCollider2D camBox;
    [SerializeField] private float thressholdX, thressholdY;
    private float sizeX, sizeY, ratio;

    private void Start()
    {
        sizeY = cam.orthographicSize * 2;
        ratio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * ratio;
        camBox.size = new Vector2(sizeX * thressholdX, sizeY * thressholdY);
    }

}
