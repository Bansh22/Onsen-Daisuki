using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Default : MonsterParent
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
    private bool onElectric =false;
    public ParticleSystem spwanpart;
    public ParticleSystem electicpart;
    void Start()
    {
        configReaders = new ConfigReader("Monster_Default");
        MonsterMaxHp = configReaders.Search<float>("Hp");
        MonsterCurrentHp = MonsterMaxHp;
        MonsterSpeed = configReaders.Search<float>("Speed");
        mf = GetComponent<MeshFilter>();
        anitor = GetComponent<Animator>(); 
        nmAgent = GetComponent<NavMeshAgent>();
        TR = GetComponent<Transform>(); 
        skin = GetComponent<SkinnedMeshRenderer>();
        // skin.materials = damagedMat;
        //DamageMat();
        nmAgent.velocity = Vector3.zero;
    }
    public override void Attack()
    {
      //  Debug.Log("�����ϱ�");
     //  throw new System.NotImplementedException();
    }

    public override void Move()
    {
        anitor.SetFloat("Speed" ,MonsterSpeed);
        //TR.position += TR.forward * Time.deltaTime * MonsterSpeed;
      //  Debug.Log("�����̱�");
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
            
            MonsterCurrentHp -= collision.gameObject.GetComponent<BulletStatus>().Damage;

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
            ElectricMat();
                anitor.speed = 0.1f;
             onElectric = true;
                StartCoroutine(AnispeedRollBack(3f));
              
            
        }


        if (collision.transform.tag == "Bullet")
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            MonsterCurrentHp -= collision.gameObject.GetComponent<BulletStatus>().Damage;
            if (MonsterCurrentHp <= 0)
            {
                //���� �����
                AudioManager.instance.Playsfx(AudioManager.Sfx.monster_dead);
                anitor.SetInteger("DamageType",3);
                DamageMat();
                StartCoroutine(WaitForAnimationToEnd(0.7f));
            }
            else
            {
                //������ �ޱ� �߰� 

                //���� �ǰ���
                AudioManager.instance.Playsfx(AudioManager.Sfx.monster_hit);
                anitor.SetInteger("DamageType", 1);
                DamageMat();
                StartCoroutine(WaitForAnimationToEnd(0.4f));
            }
        }
    }
    IEnumerator WaitForAnimationToEnd(float waitTime)
    {
        // ��� �ð���ŭ ���
        yield return new WaitForSeconds(waitTime);

        // �ִϸ��̼��� ����� �Ŀ� ����� �ڵ�
        anitor.SetInteger("DamageType", 0);
        cureMat();
        if (MonsterCurrentHp <= 0)
        {
            ParticleSystem temppart = Instantiate(spwanpart, gameObject.GetComponent<Transform>().position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(onElectric == true)
        {
            ElectricMat();
        }
        
    }
    IEnumerator AnispeedRollBack(float waitTime)
    {
        // ��� �ð���ŭ ���
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
      
        skin.materials = electricMat;//�̰͸� ���� 
    }
    public void cureMat()
    {
        
        skin.materials = tempMat;
    }
}
