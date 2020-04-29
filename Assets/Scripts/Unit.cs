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
    public int playerNumber;

    public int attackRange;
    public List<Unit> ennemiesInRange = new List<Unit>();
    public bool hasAttacked;

    public GameObject weaponIcon;

    private void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
        weaponIcon.SetActive(false);
    }

    public void Move(Vector2 position)
    {
        gameMaster.ResetTiles();
        StartCoroutine(StartMovement(position));
    }

    IEnumerator StartMovement(Vector2 position)
    {
        ResetWeaponIcons();
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
        ResetWeaponIcons();
        GetEnnemies();
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

    public void ResetWeaponIcons()
    {
        foreach (Unit ennemy in FindObjectsOfType<Unit>())
        {
            ennemy.weaponIcon.SetActive(false);
        }
        ennemiesInRange.Clear();
    }

    public void Select()
    {
        if(playerNumber == gameMaster.playerTurn)
        {
            gameMaster.selectedUnit?.Unselect();
            isSelected = true;
            gameMaster.selectedUnit = this;
            GetEnnemies();
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

    void GetEnnemies()
    {
        ennemiesInRange.Clear();
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            bool isReachable = (Mathf.Abs(transform.position.x - unit.transform.position.x) +
                Mathf.Abs(transform.position.y - unit.transform.position.y)) <= attackRange;
            if (isReachable)
            {
                if(gameMaster.playerTurn != unit.playerNumber && !hasAttacked)
                {
                    ennemiesInRange.Add(unit);
                    unit.weaponIcon.SetActive(true);
                }
            }
        }
    }
}
