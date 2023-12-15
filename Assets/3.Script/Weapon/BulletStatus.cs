using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : MonoBehaviour
{
    public int bulletnum;
    public float Damage;
    SphereCollider col;

    private void Start()
    {
        col = GetComponent<SphereCollider>();

        if(bulletnum == 1)
        {
            SkillOn();
        }

    }

    public void SkillOn()
    {
        gameObject.SetActive(true);
        ColliderOn();
        Invoke("ColliderOff", 0.2f);
        Invoke("SkillDestroy", 3.0f);
    }

    void ColliderOn()
    {
        col.enabled = true;
    }

    void ColliderOff()
    {
        col.enabled = false;
    }

    void SkillDestroy()
    {
        gameObject.SetActive(false);
    }

}
