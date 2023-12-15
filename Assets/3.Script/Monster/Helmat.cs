using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helmat : MonoBehaviour
{
    public float MonsterCurrentHp = 200;
    public float MonsterMaxHp;
    private ConfigReader configReaders;
    public Image hpimage;
    private void Awake()
    {
        configReaders = new ConfigReader("Helmat");

        MonsterCurrentHp = configReaders.Search<float>("Hp");
        MonsterMaxHp = configReaders.Search<float>("Hp"); 
    }
    public void Update()
    {
        hpimage.rectTransform.localScale = new Vector3(MonsterCurrentHp / MonsterMaxHp, 1, 1);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(collision.gameObject);
            MonsterCurrentHp -= collision.gameObject.GetComponent<BulletStatus>().Damage;
            if (MonsterCurrentHp < 0)
            {

                MonsterCurrentHp = 0;

                gameObject.transform.parent = null;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 7, ForceMode.Impulse);

                //사망 추가

                //몬스터 사망음
                AudioManager.instance.Playsfx(AudioManager.Sfx.helmet_dead);
                StartCoroutine(deletedelay(3));
            }
            else
            {
                //데미지 받기 추가 
                Debug.Log("데미지");
                //몬스터 피격음
                AudioManager.instance.Playsfx(AudioManager.Sfx.helmet_hit);
            }
        }
    }
    IEnumerator deletedelay(int waveNumber)
    {
        yield return new WaitForSeconds(waveNumber); // 0.5초 딜레이
        Destroy(gameObject);
    }
}