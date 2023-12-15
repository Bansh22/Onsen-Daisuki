using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public struct SpawnPatten
{
    public int m_wave;
    public int m_monsternum;
    public int m_spawnpos;
   

    public SpawnPatten(int a, int b, int c)
    {
        m_wave = a;
        m_monsternum = b;
        m_spawnpos = c;
      
    }
}

public class Monster_Spawn : MonoBehaviour
{
    List<Dictionary<string, object>> DataList;
    public List<SpawnPatten> SpawnContainer;
    public GameObject[] slimes;
    public GameObject[] target;
    public GameObject[] spwanpoint;
    public GameObject Endui;
    public GameObject spwanpointclone;
    public ParticleSystem spwanpart;
    public int configLineCount;
    public GameObject[] foundObjects;
    public Vector3[] spwanpointvec;
    public int waveNumber;
    public bool oncreate= false;

    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("파일 읽어오기");
        DataList = CSVReader.Read("Config");
        configLineCount = 0;

        SpawnContainer.Clear();
        SpawnPatten spawnBuf;
        int a, b, c;
        waveNumber = 1;
        foreach (var Seq in DataList)
        {
            a = int.Parse(Seq["wave"].ToString());
            b = int.Parse(Seq["monsternum"].ToString());
            c = int.Parse(Seq["spawnpos"].ToString());

            spawnBuf = new SpawnPatten(a, b, c);
            SpawnContainer.Add(spawnBuf);
        }
        spwanpointvec = new Vector3[spwanpoint.Length];
        for (int i = 0; i < spwanpoint.Length; i++) {
            spwanpointvec[i] = spwanpoint[i].GetComponent<Transform>().position;
        }
       // StartCoroutine(SpawnWavesWithDelay());
    }
    public void Update()
    {
        foundObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (oncreate == false)
        {
            // 찾은 하위 객체에 대한 작업 수행
            if (foundObjects.Length == 0)
            {
                oncreate = true;
                if (waveNumber > 8)
                {
                    Endui.SetActive(true);
                    return;
                }
                StartCoroutine(Spawn_Wavenum(waveNumber));
                waveNumber++;

            }
        }
    }
    /*
    IEnumerator SpawnWavesWithDelay()
    {
        Debug.Log("소환준비");
        yield return new WaitForSeconds(10f); // Wait for 10 seconds initially

        for (int waveNumber = 1; waveNumber <= SpawnContainer.Last().m_wave; waveNumber++)
        {
            Debug.Log("소환!");
            StartCoroutine(Spawn_Wavenum(waveNumber));
            yield return new WaitForSeconds(10f + 5 * waveNumber);
        }
        yield return new WaitForSeconds(40f); // Wait for 40 seconds before sending the next wave

        Endui.SetActive(true);
    }*/
    IEnumerator Spawn_Wavenum(int waveNumber)
    {

        Debug.Log("wave : " + waveNumber);

        while (waveNumber == SpawnContainer[configLineCount].m_wave) 
        {
            int monsterID = SpawnContainer[configLineCount].m_monsternum;

         
            int tempinttarget= UnityEngine.Random.Range(0, target.Length);
            int tempintspwan = UnityEngine.Random.Range(0, spwanpoint.Length);
            GameObject temp = Instantiate(slimes[monsterID], spwanpointvec[tempintspwan], Quaternion.identity);
            if (monsterID == 0)
            {
                temp.GetComponent<Monster_Default>().target = target[tempinttarget].transform;
                
               // temp.GetComponent<Transform>().parent = spwanpoint[tempintspwan].GetComponent<Transform>();
            }
            else if (monsterID == 1)
            {
                temp.GetComponent<Monster_helmat>().target = target[tempinttarget].transform;
              
                //  temp.GetComponent<Transform>().parent = spwanpoint[tempintspwan].GetComponent<Transform>();
            }
            else if(monsterID == 2)
            {
                temp.GetComponent<Monster_Runner>().target = target[tempinttarget].transform;
               
                //   temp.GetComponent<Transform>().parent = spwanpoint[tempintspwan].GetComponent<Transform>();
            }
           ParticleSystem temppart = Instantiate(spwanpart, spwanpointvec[tempintspwan], Quaternion.identity);

            tempinttarget = UnityEngine.Random.Range(0, target.Length);
            tempintspwan = UnityEngine.Random.Range(0, spwanpoint.Length);
            try
            {
                GameObject temp2 = Instantiate(slimes[monsterID], spwanpointvec[tempintspwan], Quaternion.identity);
                if (monsterID == 0)
                {
                    temp2.GetComponent<Monster_Default>().target = target[tempinttarget].transform;
                    
                    // temp2.GetComponent<Transform>().parent = spwanpoint[tempintspwan].GetComponent<Transform>();
                }
                else if (monsterID == 1)
                {
                    temp2.GetComponent<Monster_helmat>().target = target[tempinttarget].transform;
               
                    //  temp2.GetComponent<Transform>().parent = spwanpoint[tempintspwan].GetComponent<Transform>();
                }
                else if (monsterID == 2)
                {
                    temp2.GetComponent<Monster_Runner>().target = target[tempinttarget].transform;
                   
                    // temp2.GetComponent<Transform>().parent = spwanpoint[tempintspwan].GetComponent<Transform>();
                }
                temp2.GetComponent<Transform>().position = spwanpointvec[tempintspwan];
                ParticleSystem temppart2 = Instantiate(spwanpart, spwanpointvec[tempintspwan], Quaternion.identity);
            }
           
            catch
            {

            }
            configLineCount++;
            yield return new WaitForSeconds(0.5f); // 0.5초 딜레이
        }
        oncreate = false;
    }
}
