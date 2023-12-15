using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopUI : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale !=0 && ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            Stop();
        }
        else if(Time.timeScale == 0 && ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            Dontstop();
        }

    }
    
    public void Stop()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Dontstop()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void Golobby()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1;
    }
}
