using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster_Runner : MonsterParent
{
    // Start is called before the first frame update
   
    private ConfigReader configReaders;
    private Transform TR;
    private Animator anitor;
    private Animation anim;
    MeshFilter mf;
    public Material[] damagedMat;
    public Material[] electricMat;
    AnimatorClipInfo[] m_AnimatorClipInfo;
    private SkinnedMeshRenderer skin;
    public Material[] tempMat;
    public Material[] runMat;
    private bool onElectric = false;
    public ParticleSystem spwanpart;
    public ParticleSystem electicpart;
    public Image hpimage;
    void Start()
    {
        configReaders = new ConfigReader("Monster_Runner");
        MonsterMaxHp = configReaders.Search<float>("Hp");
        MonsterCurrentHp = MonsterMaxHp;
        MonsterSpeed = configReaders.Search<float>("Speed");
        mf = GetComponent<MeshFilter>();
        anitor = GetComponent<Animator>(); 
        nmAgent = GetComponent<NavMeshAgent>();
        TR = GetComponent<Transform>();
        target = GameManager.Instance.target.transform;
        skin = GetComponent<SkinnedMeshRenderer>();
        nmAgent.velocity = Vector3.zero;


    }
    public override void Attack()
    {
      //  Debug.Log("공격하기");
     //  throw new System.NotImplementedException();
    }

    public override void Move()
    {
        anitor.SetFloat("Speed" ,MonsterSpeed);
        //TR.position += TR.forward * Time.deltaTime * MonsterSpeed;
      //  Debug.Log("움직이기");
    }

    public override void TakeDamage()
    {
      //  throw new System.NotImplementedException();
    }

    public override void Turn()
    {
        nmAgent.SetDestination(target.position);
    }
    public void FixedUpdate()
    {
        //Fetch the Animator component from the GameObject
   
        //Get the animator clip information from the Animator Controller
       // m_AnimatorClipInfo = anitor.GetCurrentAnimatorClipInfo(0);
        //Output the name of the starting clip
       // Debug.Log("Starting clip : " + m_AnimatorClipInfo[0].clip);
      
        Move();
        Attack();
        hpimage.rectTransform.localScale = new Vector3(MonsterCurrentHp / MonsterMaxHp, 1, 1);
    }
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "terrain")
        {
            Turn();
        }
    }
    public void OnTriggerEnter(Collider collision)
    {
        
        if (collision.transform.tag == "Magic")
        {
            tempMat = runMat;
            MonsterCurrentHp -= collision.gameObject.GetComponent<BulletStatus>().Damage;
            nmAgent.speed += 2f;
            if (MonsterCurrentHp <= 0)
            {
                AudioManager.instance.Playsfx(AudioManager.Sfx.monster_dead);
                anitor.SetInteger("DamageType", 3);
                DamageMat();
                StartCoroutine(WaitForAnimationToEnd(0.7f));
            }
        }
        if (collision.transform.tag == "Electric")
        {

            ParticleSystem temppart = Instantiate(electicpart, gameObject.GetComponent<Transform>().position, Quaternion.identity);
            temppart.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
            tempMat = runMat;
            ElectricMat();
            anitor.speed = 0.1f;
            onElectric = true;
            StartCoroutine(AnispeedRollBack(3f));


        }
        if (collision.transform.tag=="Bullet")
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            MonsterCurrentHp -= collision.gameObject.GetComponent<BulletStatus>().Damage;
            tempMat = runMat;
            if (MonsterCurrentHp<0)
            {
                MonsterCurrentHp = 0;
                //사망 추가
               
                //몬스터 사망음
                AudioManager.instance.Playsfx(AudioManager.Sfx.monster_dead);
                anitor.SetInteger("DamageType",3);
                DamageMat();
                StartCoroutine(WaitForAnimationToEnd(0.7f));
            }
            else
            {
                //데미지 받기 추가 

                //몬스터 피격음
                AudioManager.instance.Playsfx(AudioManager.Sfx.monster_hit);
                anitor.SetInteger("DamageType", 1);
                DamageMat();
                StartCoroutine(WaitForAnimationToEnd(0.4f));
                nmAgent.speed += 5f;
            }
        }
    }
    
    IEnumerator WaitForAnimationToEnd(float waitTime)
    {
        // 대기 시간만큼 대기
        yield return new WaitForSeconds(waitTime);

        // 애니메이션이 종료된 후에 실행될 코드
        anitor.SetInteger("DamageType", 0);
        cureMat();
        if (MonsterCurrentHp == 0)
        {
            ParticleSystem temppart = Instantiate(spwanpart, gameObject.GetComponent<Transform>().position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (onElectric == true)
        {
            ElectricMat();
        }

    }
    IEnumerator AnispeedRollBack(float waitTime)
    {
        // 대기 시간만큼 대기
        yield return new WaitForSeconds(waitTime);
        anitor.speed = 1f;
        onElectric = false;
        cureMat();

    }
    public void DamageMat()
    {
      
        skin.materials = damagedMat;
    }
    public void ElectricMat()
    {
       
        skin.materials = electricMat;//이것만 수정 
    }
    public void cureMat()
    {

        skin.materials = tempMat;
    }
}
