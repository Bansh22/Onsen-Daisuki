using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coolcheck : MonoBehaviour
{
    public Skill skillcool;
    public Image skillimage;
    public Text buttontext;
    public Text cooltext;
    // Update is called once per frame
    void Update()
    {

        if(skillcool.currentCooldown == 0)
        {
            cooltext.gameObject.SetActive(false);
            buttontext.gameObject.SetActive(true);
        }
        else
        {
          
            cooltext.gameObject.SetActive(true);
            buttontext.gameObject.SetActive(false);
            cooltext.text = skillcool.currentCooldown.ToString("F0");
        }
        skillimage.color = new Color(0, 0, 0, (float)(255 - ((10-skillcool.currentCooldown) * 25.5)));

    }
}
