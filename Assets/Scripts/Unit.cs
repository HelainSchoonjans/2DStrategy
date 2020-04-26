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
    public int playerNumber;

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
        gameMaster.ResetTiles();
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
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        hasMoved = true;
    }

    public void Unselect()
    {
        if (isSelected)
        {
            isSelected = false;
            gameMaster.selectedUnit = null;
            gameMaster.ResetTiles();
        }
    }

    public void Select()
    {
        if(playerNumber == gameMaster.playerTurn)
        {
            gameMaster.selectedUnit?.Unselect();
            isSelected = true;
            gameMaster.selectedUnit = this;
            GetWalkableTiles();
        }
    }

    public void OnMouseDown()
    {
        if( isSelected == true )
        {
            // TODO possible to get rid of the if else and remove some code
            Unselect();
        } else
        {
            Select();
        }
    }

    void GetWalkableTiles()
    {
        if (hasMoved)
        {
            return;
        }

        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            bool isReachable = (Mathf.Abs(transform.position.x - tile.transform.position.x) +
                Mathf.Abs(transform.position.y - tile.transform.position.y)) <= tileSpeed;
            if (isReachable)
            {
                if(tile.IsClear())
                {
                    tile.Highlight();
                }
            }
        }
    }
}
