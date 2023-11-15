using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] HpBar hpBar;

    Creature _crit;

    public void SetData(Creature crit)
    {
        _crit = crit;
        
        nameText.text = crit.Base.Name;
        levelText.text = "Lvl " + crit.Level;
        hpBar.SetHP((float)crit.HP / crit.MaxHp);
    }

    public IEnumerator UpdateHp(){
        yield return hpBar.SetHPSmooth(_crit.HP / _crit.MaxHp);
    }
}
