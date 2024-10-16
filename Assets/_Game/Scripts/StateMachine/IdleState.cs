using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    private float time;
    private float countTime;
    public void OnEnter(Enemy t)
    {
        t.rb.velocity = Vector3.zero;
        t.Agent.ResetPath();
        time = Random.Range(0, 3f);
        countTime = 0f;
    }

    public void OnExecute(Enemy t)
    {
        if(countTime >= time){
            t.ChangeState(new PatrolState());
        }
        countTime += Time.deltaTime;
    }

    public void OnExit(Enemy t)
    {

    }

}
