using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
       transform.Translate(Vector3.forward * Time.deltaTime * 100.0f);
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Invoke("DestroyBullet", 0.5f);
           
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
