using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject ControlCanvas;
    public GameObject OptionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ControlcanvasOn()
    {
        ControlCanvas.SetActive(true);
        MainCanvas.SetActive(false);
        AudioManager.instance.Playsfx(AudioManager.Sfx.UI_select);
    }
    public void OptionCanvasOn()
    {
        OptionCanvas.SetActive(true);
        MainCanvas.SetActive(false);
        AudioManager.instance.Playsfx(AudioManager.Sfx.UI_select);
    }

    public void back()
    {
        OptionCanvas.SetActive(false);
        ControlCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        AudioManager.instance.Playsfx(AudioManager.Sfx.UI_close);
    }
   
}
