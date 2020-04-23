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
        Debug.Log("Initializing unit");
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

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if((Mathf.Abs(transform.position.x - tile.transform.position.x) + 
                Mathf.Abs(transform.position.y - tile.transform.position.y)) <= tileSpeed)
            {
                if(tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }
}
