using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, DICEROLL, BEHAVIOR, RESULT, WON, LOST }

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

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject PC1Go = Instantiate(PC1, PC1BattleStation);
        PC1Unit = PC1Go.GetComponent<Unit>();

        GameObject PC2Go = Instantiate(PC2, PC2BattleStation);
        PC2Unit = PC2Go.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = BattleState.DICEROLL;
        DICEROLL();
    }

    void DICEROLL()
    {
        Dice1 = Random.Range(1, 7);
        Dice2 = Random.Range(1, 7);
        
        Debug.Log(Dice1 + "vs" + Dice2);

        state = BattleState.BEHAVIOR;
        BEHAVIOR();
    }

    void BEHAVIOR()
    {
        EnemyAction();
        if(PC1Unit.PCBehavior && PC2Unit.PCBehavior)
        {
            state = BattleState.RESULT;
            RESULT();
        }
    }

    #region 적 행동
    void EnemyAction()
    {
        int enemyBehavior = Random.Range(1, 4);

        switch(enemyBehavior)
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
        if(state != BattleState.BEHAVIOR)
            return;

        if(PC1Unit.turnSkip == false)
        {
            PC1Unit.Attack = true;
            PC1Unit.PCBehavior = true;
        }
        else
        {
            PC1Unit.turnSkip = false;
            PC1Unit.PCBehavior = true;
        }

        BEHAVIOR();
    }

    public void OnDefenseButton()
    {
        if(state != BattleState.BEHAVIOR)
            return;

        if (PC1Unit.turnSkip == false)
        {
            PC1Unit.Defense = true;
            PC1Unit.PCBehavior = true;
        }
        else
        {
            PC1Unit.turnSkip = false;
            PC1Unit.PCBehavior = true;
        }

        BEHAVIOR();
    }

    public void OnCounterButton()   
    {
        if(state != BattleState.BEHAVIOR)
            return;

        if (PC1Unit.turnSkip == false)
        {
            PC1Unit.Counter = true;
            PC1Unit.PCBehavior = true;
        }
        else
        {
            PC1Unit.turnSkip = false;
            PC1Unit.PCBehavior = true;
        }

        BEHAVIOR();
    }
    #endregion

    void RESULT()
    {
        Debug.Log("결과창입니다.");

        if (PC1Unit.Attack && PC2Unit.Attack)
        {
            Debug.Log("아무일도 일어나지 않았다.");
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        if (PC1Unit.Defense && PC2Unit.Defense)
        {
            Debug.Log("아무일도 일어나지 않았다.");
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        if (PC1Unit.Counter && PC2Unit.Counter)
        {
            Debug.Log("아무일도 일어나지 않았다.");
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        if (PC1Unit.Attack && PC2Unit.Defense)
        {
            if (Dice1 > Dice2)
            {
                PC2Unit.currentHP -= Dice2 - Dice1;
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
            else
            {
                Debug.Log("아무일도 일어나지 않았다.");
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
        }

        if (PC1Unit.Defense && PC2Unit.Attack)
        {
            if (Dice1 < Dice2)
            {
                PC1Unit.currentHP -= Dice2 - Dice1;
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
            else
            {
                Debug.Log("아무일도 일어나지 않았다.");
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
        }

        if (PC1Unit.Counter && PC2Unit.Defense)
        {
            if (Dice1 < Dice2)
            {
                PC1Unit.turnSkip = true;
            }
            else
            {
                Debug.Log("아무일도 일어나지 않았다.");
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
        }

        if (PC1Unit.Defense && PC2Unit.Counter)
        {
            if (Dice1 > Dice2)
            {
                PC1Unit.turnSkip = true;
            }
            else
            {
                Debug.Log("아무일도 일어나지 않았다.");
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
        }

        if (PC1Unit.Counter && PC2Unit.Attack)
        {
            if (Dice1 > Dice2)
            {
                PC1Unit.currentHP -= Dice1 + Dice2;
            }
            else
            {
                Debug.Log("아무일도 일어나지 않았다.");
                state = BattleState.START;
                StartCoroutine(SetupBattle());
            }
        }
    }
}
