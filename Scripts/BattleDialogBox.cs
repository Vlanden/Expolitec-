using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject skillSelector;
    [SerializeField] GameObject skillDetails;

    [SerializeField] List<TMP_Text> actionTexts;
    [SerializeField] List<TMP_Text> skillTexts;

    [SerializeField] TMP_Text SPText;

    public void SetDialog(string dialog){
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog){
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled){
        dialogText.enabled = enabled;
    }
    public void EnableActionSelector(bool enabled){
        actionSelector.SetActive(enabled);
    }
    public void EnableSkillSelector(bool enabled){
        skillSelector.SetActive(enabled);
        skillDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction){
        for(int i=0; i<actionTexts.Count; ++i){
            if(i==selectedAction){
                actionTexts[i].color = highlightedColor;
            }else{
                actionTexts[i].color = Color.black;
            }
        }

    }
    public void UpdateSkillSelection(int selectedSkill, Creature crit){
        for(int i=0; i<skillTexts.Count; ++i){
            if(i==selectedSkill){
                skillTexts[i].color = highlightedColor;
            }else{
                skillTexts[i].color = Color.black;
            }
        }
        SPText.text = $"SP {crit.Skills[selectedSkill].Sp}/{crit.Base.Sp}";

    }

    public void SetSkillNames(List<Skill> skills){
        for(int i=0; i<skillTexts.Count; ++i){
            if(i<skills.Count){
                skillTexts[i].text = skills[i].Base.Name;
            }else{
                skillTexts[i].text = "-";
            }
        }

    }

}
