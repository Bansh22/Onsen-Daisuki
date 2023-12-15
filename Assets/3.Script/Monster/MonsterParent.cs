using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniVRM10;

public class MonsterParent : MonoBehaviour
{
    public float MonsterMaxHp;
    public float MonsterCurrentHp;
    public Transform target;
    public float MonsterSpeed;
    public NavMeshAgent nmAgent;

    public bool isLive;

    public virtual void Attack()
    {
      
    }

    public virtual void Move()
    {
    }

    public virtual void TakeDamage()
    {
       
    }

    public virtual void Turn()
    {
       
    }



}
