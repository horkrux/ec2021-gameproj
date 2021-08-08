using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class CombatManager : MonoBehaviour
{
    List<EnemyCharacter> registeredEnemies = new List<EnemyCharacter>();
    private RandomEncounterZone currentBattleZone;
    public CombatTutorial tut;
    public ItemPopup itemPopup;
    public GameUI gameUI;
    public PlayerCharacter playerUnit;
    public CombatUI combatUI;
    public int enemyCounter = 0;
    bool _isCombat = false;
    bool _isPlayerTurn = false;
    bool _hasMoved = false;
    bool _hasPerformedAction = false;
    bool _isCombatReady = false;
    bool _firstRound = true;
    public bool IsCombat
    {
        get { return _isCombat; }
        set { _isCombat = value; }
    }
    public bool IsPlayerTurn
    {
        get { return _isPlayerTurn; }
        set { _isPlayerTurn = value; }
    }
    public bool HasMoved
    {
        get { return _hasMoved; }
        set { _hasMoved = value; }
    }
    public bool HasPerformedAction
    {
        get { return _hasPerformedAction; }
        set { _hasPerformedAction = value; }
    }

    public bool IsCombatReady
    {
        get { return _isCombatReady; }
        set { _isCombatReady = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: Make random encounter inherit from other class and use that here instead
    public void StartCombat(RandomEncounterZone battleZone)
    {
        currentBattleZone = battleZone;
        currentBattleZone.gameObject.GetComponentsInChildren<LineRenderer>(true)[0].gameObject.SetActive(true);
        currentBattleZone.gameObject.GetComponentsInChildren<LineRenderer>(true)[1].gameObject.SetActive(true);
        gameUI.EnableEndTurnButton();
        enemyCounter = 0;
        combatUI.mode = CombatUI.CombatUIMode.Start;
        combatUI.gameObject.SetActive(true);
        _isCombat = true;
        playerUnit.gameObject.GetComponent<NavMeshAgent>().ResetPath();
        playerUnit.Moving = false;
        playerUnit.Rotating = false;
        

        //TEST
        //for (int i = 0; i < registeredEnemies.Count; i++)
        //{
            //registeredEnemies[i].movePoint = playerUnit.gameObject.transform.position;
            //registeredEnemies[i].gameObject.GetComponent<NavMeshAgent>().destination = playerUnit.gameObject.transform.position;
            //registeredEnemies[i].Moving = true;
            //registeredEnemies[i].IsTurning = true;
            //registeredEnemies[i].Target = playerUnit.gameObject;
        //}
    }

    public void CombatUIContinue()
    {
        combatUI.gameObject.SetActive(false);

        if (combatUI.mode == CombatUI.CombatUIMode.Start)
        {
            gameUI.DisableInventoryButton();
            NextEnemyAction();
        } 
        else if (combatUI.mode == CombatUI.CombatUIMode.PlayerTurn)
        {
            _isPlayerTurn = true;
            _hasMoved = false;
            _hasPerformedAction = false;
            gameUI.SwitchEndTurnButton();

            gameUI.EnableInventoryButton();
            playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[0].gameObject.SetActive(true);
            playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[1].gameObject.SetActive(true);

            if (_firstRound)
            {
                _firstRound = false;
                tut.gameObject.SetActive(true);
            }
            
        }
        else if (combatUI.mode == CombatUI.CombatUIMode.EnemyTurn)
        {
            _isPlayerTurn = false;
            NextEnemyAction();
        } 
        else
        {
            EndCombat();
        }
        
        //playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[0].gameObject.SetActive(true);
        //playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[1].gameObject.SetActive(true);
    }

    public void NextEnemyAction()
    {
        if (enemyCounter < registeredEnemies.Count)
        {
            if (!registeredEnemies[enemyCounter].IsAlive)
            {
                enemyCounter++;
                NextEnemyAction();
            } 
            else
            {
                playerUnit.CamFollowTarget = registeredEnemies[enemyCounter].gameObject;

                float distanceToTarget = Vector2.Distance(new Vector2(registeredEnemies[enemyCounter].gameObject.transform.position.x, registeredEnemies[enemyCounter].gameObject.transform.position.z), new Vector2(playerUnit.gameObject.transform.position.x, playerUnit.gameObject.transform.position.z));

                if (distanceToTarget <= registeredEnemies[enemyCounter].AttackRange)
                {
                    registeredEnemies[enemyCounter].Target = playerUnit.gameObject;
                    registeredEnemies[enemyCounter].IsCombatTurning = true;
                    registeredEnemies[enemyCounter].IsAttacking = true;
                } 
                else
                {
                    registeredEnemies[enemyCounter].movePoint = playerUnit.gameObject.transform.position;
                    registeredEnemies[enemyCounter].gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    registeredEnemies[enemyCounter].gameObject.GetComponent<NavMeshObstacle>().enabled = false;
                    registeredEnemies[enemyCounter].gameObject.GetComponent<NavMeshAgent>().destination = playerUnit.gameObject.transform.position;
                    registeredEnemies[enemyCounter].StartingPosAtTurn = registeredEnemies[enemyCounter].gameObject.transform.position;
                    registeredEnemies[enemyCounter].IsMoving = true;
                    registeredEnemies[enemyCounter].IsTurning = true;
                    registeredEnemies[enemyCounter].Target = playerUnit.gameObject;
                }
                
            }
        }
        else
        {
            SwapTurn();
        }
        
    }

    public void NextAction()
    {
        if (_isPlayerTurn)
        {
            //
        } 
        else
        {
            enemyCounter++;
            NextEnemyAction();
        }
    }

    //public void AttackFinished()
    //{

    //}

    public bool IsLastEnemyKilled()
    {
        bool allKilled = true;

        for (int i = 0; i < registeredEnemies.Count; i++)
        {
            if (registeredEnemies[i].IsAlive)
            {
                allKilled = false;
                break;
            }
                
        }

        return allKilled;
    }

    public void InitEndCombat()
    {
        gameUI.DisableEndTurnButton();

        playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[0].gameObject.SetActive(false);
        playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[1].gameObject.SetActive(false);

        combatUI.mode = CombatUI.CombatUIMode.Victory;
        combatUI.gameObject.SetActive(true);
    }

    public void EndCombat()
    {
        //gameUI.DisableEndTurnButton();
        _isCombat = false;
        playerUnit.gameObject.GetComponent<Inventory>().AddItem(0, 3);
        List<Tuple<int, int>> rewards = new List<Tuple<int, int>>();
        rewards.Add(new Tuple<int, int>(0, 3));
        itemPopup.Show(rewards);

        for (int i = 0; i < registeredEnemies.Count; i++)
        {
            Destroy(registeredEnemies[i].gameObject);
        }
        registeredEnemies.Clear();
        Destroy(currentBattleZone.gameObject);
    }

    public Vector2 GetBattleAreaDims()
    {
        Vector2 dims = Vector2.zero;

        if (currentBattleZone)
        {
            dims = new Vector2(currentBattleZone.transform.localScale.x, currentBattleZone.transform.localScale.z);
        }
            
        return dims;
    }

    public Vector3 GetBattleAreaPos()
    {
        Vector3 pos = Vector3.zero;

        if (currentBattleZone)
        {
            pos = currentBattleZone.transform.position;
        }
        return pos;
    }

    public bool EnemyInRange(GameObject enemy)
    {
        if (Vector2.Distance(new Vector2(playerUnit.gameObject.transform.position.x, playerUnit.gameObject.transform.position.z), new Vector2(enemy.transform.position.x, enemy.transform.position.z)) <= playerUnit.AttackRange)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    public void PlayerMoved()
    {
        playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[0].gameObject.SetActive(false);
        _hasMoved = true;

    }

    public void ExecAttackForPlayer(EnemyCharacter target)
    {
        if (registeredEnemies.Contains(target))
        {
            playerUnit.Attack(target);
        }
    }

    public void ExecAttackForEnemy(EnemyCharacter attacker)
    {
        if (registeredEnemies.Contains(attacker))
        {
            attacker.Attack(playerUnit);
        }
    }

    public void TurnPlayerCharacter(EnemyCharacter target)
    {
        playerUnit.Target = target.gameObject;
        playerUnit.RotatingCombatTarget = true;
    }

    public void TurnEnemyCharacter()
    {

    }

    public void RegisterEnemy(EnemyCharacter enemy)
    {
        registeredEnemies.Add(enemy);
    }

    public bool IsPositionWithinBattleZone(Vector3 position)
    {
        Vector3 pos = Vector3.zero;
        Vector2 dims = Vector2.zero;

        if (currentBattleZone)
        {
            pos = currentBattleZone.gameObject.transform.position;
            dims = new Vector2(currentBattleZone.transform.localScale.x, currentBattleZone.transform.localScale.z);

            if ((position.x > pos.x - dims.x / 2 && position.z > pos.z - dims.y / 2) && (position.x < pos.x + dims.x / 2 && position.z < pos.z + dims.y / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        else
        {
            return false;
        }
    }

    // Change variables here
    public void SwapTurn()
    {
        enemyCounter = 0;

        if (!_isPlayerTurn)
        {
            combatUI.mode = CombatUI.CombatUIMode.PlayerTurn;
            combatUI.gameObject.SetActive(true);
            playerUnit.ReCenterCam = true;
            playerUnit.CamFollowTarget = null;
        } 
        else
        {
            playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[0].gameObject.SetActive(false);
            playerUnit.gameObject.GetComponentsInChildren<Projector>(true)[1].gameObject.SetActive(false);
            combatUI.mode = CombatUI.CombatUIMode.EnemyTurn;
            combatUI.gameObject.SetActive(true);
        }
    }
}
