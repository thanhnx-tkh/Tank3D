using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private TankShootingEnemy shooting;
    [SerializeField] private float attackSpeed;
    public float AttackSpeed => attackSpeed;
    public Rigidbody rb;
    public TankShootingEnemy Shooting => shooting;
    public GameObject Player => player;
    public NavMeshAgent Agent => agent;
    private IState<Enemy> currentState;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if(!GameManager.IsState(GameState.GamePlay)) return;
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Enemy> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public Vector3 GetRandomPositionBot()
    {
        Vector3 randomPosition = Vector3.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10f;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
            {
                randomPosition = hit.position;
                validPosition = true;
            }
        }

        return randomPosition;
    }
    public void FollowPlayer()
    {
        if (player != null)
        {
            Agent.SetDestination(player.transform.position);
        }
    }
}
