using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle_donggi : MonoBehaviour
{
    public Castle cadtlemain;
    public float Life;

    public float invincibilityTime = 0.1f; // ���� �ð� (��)
    private bool isInvincible = false;   // ���� ���� ����
    private float invincibilityTimer = 0f; // ���� Ÿ�̸�



    public void OnCollisionEnter(Collision collision)
    {
       
            cadtlemain.OnCollisionEnter(collision);
          
        
    }

   
}
