using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle_donggi : MonoBehaviour
{
    public Castle cadtlemain;
    public float Life;

    public float invincibilityTime = 0.1f; // 무적 시간 (초)
    private bool isInvincible = false;   // 무적 상태 여부
    private float invincibilityTimer = 0f; // 무적 타이머



    public void OnCollisionEnter(Collision collision)
    {
       
            cadtlemain.OnCollisionEnter(collision);
          
        
    }

   
}
