using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CreateStats _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayer;

    public Creature crit { get; set; }

    public void Setup(){
        crit = new Creature(_base,level);

        GetComponent<Image>().sprite = crit.Base.Face;
    }
}
