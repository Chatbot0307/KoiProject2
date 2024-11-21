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
        if(PC1Unit.PCBehavior == true && PC2Unit.PCBehavior == true)
        {
            state = BattleState.RESULT;
            RESULT();
        }
    }   

    void EnemyAction()
    {
        int enemyBehavior = Random.Range(1, 4);

        if(enemyBehavior == 1)
        {
            PC2Unit.Attack = true;
            PC2Unit.PCBehavior = true;
        }

        else if (enemyBehavior == 2)
        {
            PC2Unit.Defense = true;
            PC2Unit.PCBehavior = true;
        }

        else if (enemyBehavior == 3)
        {
            PC2Unit.Counter = true;
            PC2Unit.PCBehavior = true;
        }
    }
    
    public void OnAttackButton()
    {
        if(state != BattleState.BEHAVIOR)
            return;
        PC1Unit.Attack = true;
        PC1Unit.PCBehavior = true;
    }

    public void OnDefenseButton()
    {
        if(state != BattleState.BEHAVIOR)
            return;
        PC1Unit.Defense = true;
        PC1Unit.PCBehavior = true;
    }

    public void OnCounterButton()   
    {
        if(state != BattleState.BEHAVIOR)
            return;
        PC1Unit.Counter = true;
        PC1Unit.PCBehavior = true;
    }

    void RESULT()
    {
        Debug.Log("���â�Դϴ�.");
        if(PC1Unit.Attack && PC2Unit.Attack)
        {
            Debug.Log("�ƹ��ϵ� �Ͼ�� �ʾҴ�");
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        if (PC1Unit.Defense && PC2Unit.Defense)
        {
            Debug.Log("�ƹ��ϵ� �Ͼ�� �ʾҴ�");
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        if (PC1Unit.Counter && PC2Unit.Counter)
        {
            Debug.Log("�ƹ��ϵ� �Ͼ�� �ʾҴ�");
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }
    }
}
