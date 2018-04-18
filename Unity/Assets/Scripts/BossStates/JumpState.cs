using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IEnemyState
{
    private Boss boss;
    int jumpCounter = 0;
    int jumps = 3;
    private float jumpTimer;
    private float jumpCooldown = 1F;

    public void Enter(Boss boss)
    {
        this.boss = boss; 
    }

    public void Execute()
    {
        if (boss.health > 0)
        {
            boss.Move();
            Jump();
            Enrage();
        }
    }

    public void Exit()
    { 
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "SupergirlFoot")
        {
            boss.ChangeState(new EnragedState());
        }
    }

    public void OnCollisionEnter2D(Collider2D other)
    {
        if (other.tag == "SupergirlFoot")
        {
            boss.ChangeState(new EnragedState());
        }
    }

    public void Jump()
    {
        //Debug.Log("I am jump");
        for (int i = 0; i == 1; i++)
            boss.Jump();
        jumpTimer += Time.deltaTime;
        if (boss.IsGrounded() == true && jumpTimer >= jumpCooldown)
        {
            //Debug.Log("JUMP2");
            boss.Jump();
            jumpCounter += 1;
            jumpTimer = 0;
        }
        if (jumpCounter == jumps)
        {
            //Debug.Log("Done Jumping");
            boss.GetComponent<Animator>().ResetTrigger("Jump");
            boss.ChangeState(new MoveState());
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
