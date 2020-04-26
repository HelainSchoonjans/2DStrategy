using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] tileGraphics;

    public LayerMask obstacleLayer;

    public Color highlightedColor;
    public bool isWalkable;
    GameMaster gameMaster;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randomTile = Random.Range(0, tileGraphics.Length);
        rend.sprite = tileGraphics[randomTile];

        gameMaster = FindObjectOfType<GameMaster>();
    }

    public void OnMouseDown()
    {
        if(isWalkable && gameMaster.selectedUnit != null)
        {
            gameMaster.selectedUnit.Move(this.transform.position);
        }
    }

    public bool IsClear()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        return col == null;
    }

    public void Highlight()
    {
        rend.color = highlightedColor;
        isWalkable = true;
    }

    public void Reset()
    {
        rend.color = Color.white;
        isWalkable = false;
    }
}
