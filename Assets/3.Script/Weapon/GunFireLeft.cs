using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GunFireLeft: MonoBehaviour
    
{
    private ConfigReader configReaders;
    public static GunFire instance;
    public GameObject bullet_right; 
    public GameObject Gun;
    public GameObject Bullet;
    public GameObject Bullet_inside;
    public GameObject BulletRight_Hand;
    public int bulletcount = 25;
    private float shootdelay = 0.2f;
    private float shootcool = 0.3f;
    private bool shootready = true;
    private float reloadDelay = 0.5f;
    private float reloadcool = 0.0f;
    private bool reloadready = false;


#if PC
    // Start is called before the first frame update
    void Start()
    {
        transform.position = PC_Rhand.transform.position;
        transform .rotation = PC_Rhand.transform.rotation;
        transform.parent = PC_Rhand.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            Instantiate(bullet_right, transform.position, transform.rotation);
        } 
    }
#endif




    void Start()
    {
        transform.position = Bullet.transform.position;
        transform.rotation = Bullet.transform.rotation;
        transform.parent = Gun.transform;
    }
    public void Update()
    {        
        shootcool += Time.deltaTime;
        shootready = shootdelay < shootcool;
        TriggerShoot();
        try
        {
            if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.LTouch))
            {                
                ReloadGun();
            }
        }
        catch
        {

        }

    }
    public void TriggerShoot()
    {
        Ray ray = new Ray(Bullet.transform.position, -Bullet.transform.right);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "Reload")
            {
                if (bulletcount != 25)
                {
                    AudioManager.instance.Playsfx(AudioManager.Sfx.reload);
                }
                reloadcool += Time.deltaTime;
                if (reloadcool > reloadDelay)
                {
                    bulletcount = 25;
                    reloadcool = 0;
                }

            }
            else
            {
                reloadcool = 0;
            }
        }
    }
    public void test()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            GameObject temp = Instantiate(bullet_right, transform.position, transform.rotation);
            temp.transform.forward = (Bullet.transform.position - Bullet_inside.transform.position).normalized;
            ARAVRInput.PlayVibration(0.06f, 1, 1, ARAVRInput.Controller.RTouch);
        }
    }
    public  void Shoot()
    {

        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            if (shootready) {
                GameObject temp = Instantiate(bullet_right, transform.position, transform.rotation);
                temp.transform.forward = (Bullet.transform.position - Bullet_inside.transform.position).normalized;
                ARAVRInput.PlayVibration(0.06f, 1, 1, ARAVRInput.Controller.RTouch);
                shootcool = 0f;
            }
        }
    }
    public void Shoot1()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            GameObject temp = Instantiate(bullet_right, transform.position, transform.rotation);
            temp.transform.forward = (Bullet.transform.position - Bullet_inside.transform.position).normalized;
            ARAVRInput.PlayVibration(0.06f, 1, 1, ARAVRInput.Controller.RTouch);
        }
    }

    void ReloadGun()
    {
        

        if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.LTouch)&& bulletcount > 0)
            {
            if (shootready)
            {
                AudioManager.instance.Playsfx(AudioManager.Sfx.pistol);
                GameObject temp = Instantiate(bullet_right, transform.position, transform.rotation);
                configReaders = new ConfigReader("Bullet_0");
                temp.GetComponent<BulletStatus>().Damage = configReaders.Search<float>("Damage"); 
                temp.transform.forward = (Bullet.transform.position - Bullet_inside.transform.position).normalized;
                ARAVRInput.PlayVibration(0.06f, 1, 1, ARAVRInput.Controller.LTouch);
                bulletcount--;
                shootcool = 0f;
            }
        }                
    }
    
}
