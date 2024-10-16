using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{

    private Vector3 destination;
    private float timer;
    private float moveTime;
    public void OnEnter(Enemy t)
    {
        timer = 0;
        moveTime = Random.Range(5f,10f);
        destination = t.GetRandomPositionBot();
        t.Agent.SetDestination(destination);
    }

    public void OnExecute(Enemy t)
    {
        if (Vector3.Distance(destination, t.transform.position) < 0.1f){
            destination = t.GetRandomPositionBot();
            t.Agent.SetDestination(destination);
        }
        if(timer > moveTime)
        {
            t.ChangeState(new IdleState());
        }
        timer += Time.deltaTime;
    }
    public void OnExit(Enemy t)
    {

    }

}