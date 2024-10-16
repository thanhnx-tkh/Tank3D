using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.rb.velocity =Vector3.zero;
        t.Agent.ResetPath();
    }

    public void OnExecute(Enemy t)
    {
       t.Shooting.Turret();
    }

    public void OnExit(Enemy t)
    {

    }
}
