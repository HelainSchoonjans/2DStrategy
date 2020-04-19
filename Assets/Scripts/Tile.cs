using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] tileGraphics;

    public float hoverAmount;

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

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }

    public void OnMouseDown()
    {
        Debug.Log("Clicking on tile");
    }

    public bool IsClear()
    {
        Debug.Log("Is clear");
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        if(col != null)
        {
            return false;
        } else
        {
            return true;
        }
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
