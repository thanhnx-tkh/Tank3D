using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    private float time;
    private float countTime;
    public void OnEnter(Enemy t)
    {
        t.rb.velocity =Vector3.zero;
        t.Agent.ResetPath();
        time = t.AttackSpeed;
        countTime = 0;
        t.RotationPlayer();
        t.Shooting.Fire();
    }

    public void OnExecute(Enemy t)
    {
        if(countTime >= time){
            t.ChangeState(new AttackState());
        }

        countTime += Time.deltaTime;  
    }

    public void OnExit(Enemy t)
    {

    }
}
