using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, ResetTurn, DiceRoll, Action, Result, Won, Lost }

public class BattleSystem : MonoBehaviour
{
    public GameObject PC1;
    public GameObject PC2;

    public Transform PC1BattleStation;
    public Transform PC2BattleStation;

    Unit PC1Unit;
    Unit PC2Unit;

    public int Dice1;
    public int Dice2;

    public BattleState state;
    public int Turn;

    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject PC1Go = Instantiate(PC1, PC1BattleStation);
        PC1Unit = PC1Go.GetComponent<Unit>();

        GameObject PC2Go = Instantiate(PC2, PC2BattleStation);
        PC2Unit = PC2Go.GetComponent<Unit>();

        yield return new WaitForSeconds(1f);

        state = BattleState.DiceRoll;
        DiceRoll();
    }

    void ResetTurn()
    {
        Debug.Log("PC1체력 : " + PC1Unit.currentHP + "\nPC2체력 : " + PC2Unit.currentHP);

        PC1Unit.Attack = false;
        PC2Unit.Attack = false;

        PC1Unit.Defense = false;
        PC2Unit.Defense = false;

        PC1Unit.Counter = false;
        PC2Unit.Counter = false;

        PC1Unit.PCBehavior = false;
        PC2Unit.PCBehavior = false;

        Turn++;

        state = BattleState.DiceRoll;
        Invoke("DiceRoll", 1f);
    }

    void DiceRoll()
    {
        Dice1 = Random.Range(1, 7);
        Dice2 = Random.Range(1, 7);
        
        Debug.Log(Dice1 + "vs" + Dice2);

        state = BattleState.Action;
        Invoke("Action", 1f);
    }

    void Action()
    {
        EnemyAction();  
        if(PC1Unit.PCBehavior && PC2Unit.PCBehavior)
        {
            state = BattleState.Result;
            Invoke("Result", 1f);
        }
    }

    #region 적 행동
    void EnemyAction()
    {
        int enemyBehavior = Random.Range(1, 4);

        switch (enemyBehavior)
        {
            case 1:
                PC2Unit.Attack = true;
                PC2Unit.PCBehavior = true;
                break;
            case 2:
                PC2Unit.Defense = true;
                PC2Unit.PCBehavior = true;
                break;
            case 3:
                PC2Unit.Counter = true;
                PC2Unit.PCBehavior = true;
                break;
        }
    }
    #endregion

    #region 버튼
    public void OnAttackButton()
    {
        if(state != BattleState.Action)
            return;

        if(PC1Unit.turnSkip != true)
        {
            PC1Unit.Attack = true;
            PC1Unit.PCBehavior = true;
        }
        else
        {
            PC1Unit.PCBehavior = true;
        }

        Action();
    }

    public void OnDefenseButton()
    {
        if(state != BattleState.Action)
            return;

        if (PC1Unit.turnSkip != true)
        {
            PC1Unit.Defense = true;
            PC1Unit.PCBehavior = true;
        }
        else
        {
            PC1Unit.PCBehavior = true;
        }

        Action(); 
    }

    public void OnCounterButton()   
    {
        if(state != BattleState.Action)
            return;

        if (PC1Unit.turnSkip != true)
        {
            PC1Unit.Counter = true;
            PC1Unit.PCBehavior = true;
        }
        else
        {
            PC1Unit.PCBehavior = true;
        }

        Action();
    }
    #endregion

    void Result()
    {
        Debug.Log("결과창입니다.");

        if(PC1Unit.turnSkip)
        {
            PC1Unit.turnSkip = false;
        }

        if (PC2Unit.turnSkip)
        {
            PC2Unit.turnSkip = false;
        }

        if (Dice1 > Dice2)
        {
            PC1Win();
        }
        else if(Dice1 < Dice2)
        {
            PC2Win();
        }
    }

    void PC1Win()
    {
        if(PC1Unit.Attack && PC2Unit.Attack)
        {
            PC2Unit.currentHP -= Dice1;
        }
    }

    void PC2Win()
    {
        
    }
}