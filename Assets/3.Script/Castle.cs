using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public float Life;

    public float invincibilityTime = 0.1f; // ���� �ð� (��)
    private bool isInvincible = false;   // ���� ���� ����
    private float invincibilityTimer = 0f; // ���� Ÿ�̸�

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

        // ���� �ð� ���� ���
        while (invincibilityTimer < invincibilityTime)
        {
            invincibilityTimer += Time.deltaTime;
            yield return null;
        }

        isInvincible = false; // ���� ���� ����
    }
}
