using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnragedState : IEnemyState
{
    private Boss boss;
    private float moveTimer;
    private float moveDuration = 2F;

    public void Enter(Boss boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        EnragedMove();
        EnragedTimer();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    public void EnragedMove()
    {
        //Debug.Log("I am Enraged.");
        boss.Move();
        boss.moveSpeed = 6;
        moveTimer += Time.deltaTime;
        boss.GetComponent<Animator>().SetBool("Arms", true);
    }

    public void EnragedTimer()
    {
        if (moveTimer >= moveDuration)
        {
            //Debug.Log("No longer angry");
            boss.enraged = false;
            boss.moveSpeed = 2;
            boss.GetComponent<Animator>().SetBool("Arms", false);
            boss.ChangeState(new MoveState());
        }
    }
}
