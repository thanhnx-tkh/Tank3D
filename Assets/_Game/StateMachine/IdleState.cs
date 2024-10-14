using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.ChangeState(new PatrolState());
    }

    public void OnExecute(Enemy t)
    {

    }

    public void OnExit(Enemy t)
    {

    }

}
