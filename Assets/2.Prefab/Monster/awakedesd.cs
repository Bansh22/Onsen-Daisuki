using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class awakedesd : MonoBehaviour
{
    public float deadcount;
    public float count=2f;

    private void Update()
    {
        deadcount += Time.deltaTime;
        if(deadcount > count)
        {
            Destroy(gameObject);
        }
    }
}
