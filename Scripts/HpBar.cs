using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHP(float hpNormalized){
        health.transform.localScale = new Vector3(hpNormalized, 0.9f);
    }
    public IEnumerator SetHPSmooth(float newHp){
        float curHp = health.transform.localScale.x;
        float changeAmt = curHp - newHp;

        while(curHp - newHp > Mathf.Epsilon){
            curHp -= changeAmt*Time.deltaTime;
            health.transform.localScale = new Vector3(curHp, 0.9f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHp, 0.9f);
    }
}
