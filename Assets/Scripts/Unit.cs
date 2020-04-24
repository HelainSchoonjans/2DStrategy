using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isSelected;
    GameMaster gameMaster;

    public int tileSpeed;
    public float moveSpeed;
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

    public void Move(Vector2 position)
    {
        StartCoroutine(StartMovement(position));
    }

    IEnumerator StartMovement(Vector2 position)
    {
        while(transform.position.x != position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(position.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, position.x), moveSpeed * Time.deltaTime);
            yield return null;
        }
        hasMoved = true;
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
        if (hasMoved == true)
        {
            return;
        }

        Tile[] tiles = FindObjectsOfType<Tile>();
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
                if(tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }
}
