using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� �Ŵ����� �ν��Ͻ��� ������ ���� ����
    private static GameManager instance;
    public GameObject target;
    public GameObject spawn;
    public GameObject[] gun;
    // �ٸ� ��ũ��Ʈ���� ���� ������ �Ӽ�
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        // �� ������Ʈ�� ������ ������Ʈ�� �����ǵ��� ��
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
       AudioManager.instance.PlayBgm(true, 2);
    }

}
