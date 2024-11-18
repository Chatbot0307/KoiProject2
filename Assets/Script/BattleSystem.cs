using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, DICEROLL, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject PC1;
    public GameObject PC2;

    public Transform PC1BattleStation;
    public Transform PC2BattleStation;

    Unit PC1Unit;
    Unit PC2Unit;

    public BattleState state;

    private int Dice1;
    private int Dice2;
 
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
    }

    void PlayerTrun()
    {
        
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
    }
}
