using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, DICEROLL, BEHAVIOUR, RESULT, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject PC1;
    public GameObject PC2;

    public Transform PC1BattleStation;
    public Transform PC2BattleStation;

    Unit PC1Unit;
    Unit PC2Unit;

    public BattleState state;

    public int Dice1;
    public int Dice2;

    public bool PC1Behaviour;
    public bool PC2Behaviour;

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

        Debug.Log(Dice1);
        Debug.Log(Dice2);

        state = BattleState.BEHAVIOUR;
        BEHAVIOUR();
    }

    void BEHAVIOUR()
    {
        if(PC1Behaviour && PC2Behaviour)
        {
            state = BattleState.RESULT;
            RESULT();
        }
    }

    public void OnAttackButton()
    {
        if(state != BattleState.BEHAVIOUR)
            return;
        PC1Unit.Attack = true;
    }

    public void OnDefenseButton()
    {
        if(state != BattleState.BEHAVIOUR)
            return;
        PC1Unit.Defense = true;
    }

    public void OnCounterButton()
    {
        if(state != BattleState.BEHAVIOUR)
            return;
        PC1Unit.Counter = true;
    }

    void RESULT()
    {
        
    }
}
