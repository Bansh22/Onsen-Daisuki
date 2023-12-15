using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleUI : MonoBehaviour
{
    public Castle castleData;
    public Monster_Spawn spawndata;
    public GameObject Hpbar;
    public Text HPtextdata;
    public Text Wavetextdata;
    public void Update()
    {
        Hpbar.GetComponent<RectTransform>().localScale = new Vector3(castleData.Life/10*3, 3f, 1f);
        HPtextdata.text = (castleData.Life / 10 * 100).ToString() + "%";
        Wavetextdata.text = "Wave " + (spawndata.waveNumber-1).ToString();
    }
}
