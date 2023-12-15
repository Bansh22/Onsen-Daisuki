#define Oculus
//#define PC
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShojoController : MonoBehaviour
{
    public List<Skill> skillSet;
    public GameObject target;

    private Vector3 shotDir, shotDest;
    private LineRenderer lr;
    private Ray ray;
    private RaycastHit hitInfo;
    

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
#if PC
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lr.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            lr.enabled = true;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            RayPosSet(0);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            RayPosSet(1);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lr.enabled = false;
            skillSet[0].Play(shotDest, shotDir);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            lr.enabled = false;
            skillSet[1].Play(shotDest, shotDir);
        }

#endif

#if Oculus
        
        if (skillSet[0].CoolTimeCheck())
        {
            if (ARAVRInput.Get(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
            {
                target.SetActive(true);
                lr.enabled = true;
                RayPosSet(0);
            }

            if (ARAVRInput.GetUP(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
            {
                lr.enabled = false;
                target.SetActive(false);
                skillSet[0].Play(shotDest, shotDir);
            }
        }

        if (skillSet[1].CoolTimeCheck())
        {
            if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.LTouch))
            {
                skillSet[1].gameObject.GetComponent<Animator>().SetTrigger("LockOn");
            }

            if (ARAVRInput.Get(ARAVRInput.Button.Two, ARAVRInput.Controller.LTouch))
            {
                lr.enabled = true;
                RayPosSet(1);
            }

            if (ARAVRInput.GetUP(ARAVRInput.Button.Two, ARAVRInput.Controller.LTouch))
            {
                lr.enabled = false;
                target.SetActive(false);

                skillSet[1].gameObject.GetComponent<Animator>().SetTrigger("Shot");
                skillSet[1].Play(shotDest, shotDir);
            }

        }
#endif
        void RayPosSet(int skillNum)
        {
#if PC
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif

#if Oculus
            if(skillNum == 0)
            {
                ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            }
            else if (skillNum == 1) 
            {
                ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            }
#endif

            int layer = 1 << LayerMask.NameToLayer("Terrain");

            if (Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                lr.SetPosition(0, skillSet[skillNum].SkillRayPos.transform.position);
                lr.SetPosition(1, hitInfo.point);
                target.transform.position = hitInfo.point;
            }

            float girlrot = (450.1f - (Mathf.Atan2(shotDir.normalized.z, shotDir.normalized.x) * Mathf.Rad2Deg)) % 360.0f;
            skillSet[skillNum].gameObject.transform.rotation = Quaternion.Euler(0f, girlrot, 0f);

            shotDest = hitInfo.point;
            shotDir = hitInfo.point - skillSet[skillNum].SkillRayPos.transform.position;
            shotDir = shotDir.normalized;
        }

    }
}
