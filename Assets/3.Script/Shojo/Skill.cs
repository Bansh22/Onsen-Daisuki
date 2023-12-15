using BigRookGames.Weapons;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Skill : MonoBehaviour
{
    public int SkillNum;
    public GameObject SkillObject;
    public GameObject SkillRayPos;
    public float SkillCooldown;
    public float currentCooldown;
    public bool reloadVoiceCheck; 

    private GunfireController gun;
    private ParticleSystem particle;

    private void Start()
    {
        reloadVoiceCheck = false;

        if (SkillCooldown == 0)
        {
            SkillCooldown = 10.0f;
        }

        if (SkillNum == 1)
        {
            gun = SkillObject.GetComponent<GunfireController>();
        }
        else if (SkillNum == 2)
        {
            particle = SkillObject.GetComponent<ParticleSystem>();
        }
    }

    public void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown < 0)
        {
            currentCooldown = 0;
        }
    }

    public bool CoolTimeCheck()
    {
        if (currentCooldown <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Play(Vector3 pos, Vector3 dir)
    {

        switch (SkillNum)
        {
            case 1:
                gun.Play(dir);
                StartCoroutine(voicePlay(1));
                break;
            case 2:
                particle.transform.position = pos;
                particle.GetComponent<BulletStatus>().SkillOn();
                particle.Play();
                AudioManager.instance.Playsfx(AudioManager.Sfx.lightning);
                StartCoroutine(voicePlay(2));
                break;
            default:
                break;
        }

        currentCooldown = SkillCooldown;

    }


    IEnumerator voicePlay(int num)
    {
        yield return new WaitForSeconds(SkillCooldown);

        if(num == 1)
        {
            AudioManager.instance.Playsfx(AudioManager.Sfx.pink_voice);
        }
        else if(num == 2) 
        {
            AudioManager.instance.Playsfx(AudioManager.Sfx.rabbit_voice);
        }
    }


}
