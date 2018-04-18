using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Boss boss;
    private float idleTimer;
    private float idleDuration = 1F;

    public void Enter(Boss boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        //Debug.Log("I am Idleing");
        Idle();
        boss.ChangeState(new MoveState()); 
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        boss.myAnimator.SetFloat("Speed", 0);
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration)
        {
            boss.ChangeState(new MoveState());
        }
    }

}
