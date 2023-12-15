using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public float Life;

    public float invincibilityTime = 0.1f; // 무적 시간 (초)
    private bool isInvincible = false;   // 무적 상태 여부
    private float invincibilityTimer = 0f; // 무적 타이머

    public void Update()
    {
        if(Life == 0)
        {
            Endgame();
        }
    }
    public void Endgame()
    {
        SceneManager.LoadScene("Lobby");        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && !isInvincible)
        {
            Life -= 1;
            Destroy(collision.gameObject);
            StartCoroutine(InvincibilityCooldown());
        }
    }

    IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;
        invincibilityTimer = 0f;

        // 무적 시간 동안 대기
        while (invincibilityTimer < invincibilityTime)
        {
            invincibilityTimer += Time.deltaTime;
            yield return null;
        }

        isInvincible = false; // 무적 상태 해제
    }
}
