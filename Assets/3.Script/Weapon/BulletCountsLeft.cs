using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountsLeft : MonoBehaviour
{
    public Text text;
    public GameObject gun;
    
    // Start is called before the first frame update
    void Start()
    {
       // text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = gun.GetComponent<GunFireLeft>().bulletcount.ToString();
    }
}
