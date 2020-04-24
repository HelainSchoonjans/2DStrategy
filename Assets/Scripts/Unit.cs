using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isSelected;
    GameMaster gameMaster;

    public int tileSpeed;
    public bool hasMoved;
    public float hoverAmount;

    private void Start()
    {
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
        if( isSelected == true )
        {
            isSelected = false;
            gameMaster.selectedUnit = null;
        } else
        {
            if(gameMaster.selectedUnit != null)
            {
                gameMaster.selectedUnit.isSelected = false;
            }
            isSelected = true;
            gameMaster.selectedUnit = this;
            GetWalkableTiles();
        }
    }

    void GetWalkableTiles()
    {
        Debug.Log("Getting walkable tiles");
        if (hasMoved == true)
        {
            return;
        }

        Tile[] tiles = FindObjectsOfType<Tile>();
        Debug.Log("Number of tiles:");
        Debug.Log(tiles.Length);
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            Debug.Log(Mathf.Abs(transform.position.x - tile.transform.position.x));
            Debug.Log(Mathf.Abs(transform.position.y - tile.transform.position.y));
            bool isReachable = (Mathf.Abs(transform.position.x - tile.transform.position.x) +
                Mathf.Abs(transform.position.y - tile.transform.position.y)) <= tileSpeed;
            Debug.Log(isReachable);
            if (isReachable)
            {
                Debug.Log("Checking for tile");
                if(tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }
}
