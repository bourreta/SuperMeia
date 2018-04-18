using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IEnemyState
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
        if (boss.health > 0)
        {
            //Debug.Log("I am Moving");
            if (boss.health <= 50)
            {
                boss.ChangeState(new RangeState());
            }
            Move();
            boss.Move();
            Enrage();
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    public void OnCollisionEnter2D(Collider2D other)
    {
    }

    private void Move()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration)
        {
            boss.ChangeState(new JumpState());
        }
    }

    public void Enrage()
    {
        if (boss.enraged == true)
        {
            boss.ChangeState(new EnragedState());
        }
    }
}
