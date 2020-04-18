using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer renderer;
    public Sprite[] tileGraphics;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        int randomTile = Random.Range(0, tileGraphics.Length);
        renderer.sprite = tileGraphics[randomTile];
    }
}
