using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임 매니저의 인스턴스를 저장할 정적 변수
    private static GameManager instance;
    public GameObject target;
    public GameObject spawn;
    public GameObject[] gun;
    // 다른 스크립트에서 접근 가능한 속성
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
        // 이 오브젝트가 유일한 오브젝트로 유지되도록 함
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
