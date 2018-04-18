using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : IEnemyState
{
    private Boss boss;
    private float shootTimer;
    private float shotCooldown = 2F;
    private float triggerCooldown;

    public void Enter(Boss boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        //Debug.Log("I am in range");
        boss.Move();
        Shoot();
        Enrage();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "SupergirlFoot")
        {
            //Debug.Log("FOOTY");
            boss.ChangeState(new EnragedState());
        }
    }

    public void Shoot()
    {
        //Debug.Log("I am Shooting");
        shootTimer += Time.deltaTime;

        //for (int i = 0; i < 1; i++)
        //{
        //    if (boss.health >= 50)
        //    {
        //        boss.Shoot();
        //    }
        //    else
        //    {
        //        boss.DoubleShoot();
        //    }
        //}

        if (shootTimer >= shotCooldown)
        {
            if (boss.health > 0)
            {
                if (boss.health >= 50)
                {
                    //Debug.Log("SHOT");
                    boss.Shoot();
                    shootTimer = 0;
                    triggerCooldown += Time.deltaTime;
                }
                else
                {
                    //Debug.Log("SHOT2");
                    boss.DoubleShoot();
                    shootTimer = 0;
                    triggerCooldown += Time.deltaTime;
                }
            }
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
