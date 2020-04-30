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

    public int health;
    public int attackDamage;
    public int counterAttackDamage;
    public int armor;

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

        Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = collider.GetComponent<Unit>();
        if(gameMaster.selectedUnit != null)
        {
            if(gameMaster.selectedUnit.ennemiesInRange.Contains(unit) && !gameMaster.selectedUnit.hasAttacked)
            {
                gameMaster.selectedUnit.Attack(unit);
            }
        }
    }

    void Attack(Unit enemy)
    {
        hasAttacked = true;

        int enemyDamage = attackDamage - enemy.armor;
        int myDamage = enemy.counterAttackDamage - armor;
        if(enemyDamage >= 1)
        {
            enemy.health -= enemyDamage;
        }
        if(myDamage >= 1)
        {
            health -= myDamage;
        }

        if(enemy.health <= 0)
        {
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }
        if(health <= 0 )
        {
            gameMaster.ResetTiles();
            Destroy(this.gameObject);
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
