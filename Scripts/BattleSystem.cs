using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {Start,PlayerAction,PlayerSkill,EnemySkill,Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleHUD enemyHUD;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    int currentAction;
    int currentSkill;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHUD.SetData(playerUnit.crit);
        enemyHUD.SetData(enemyUnit.crit);

        dialogBox.SetSkillNames(playerUnit.crit.Skills);

        yield return dialogBox.TypeDialog($"Tocaste a {enemyUnit.crit.Base.Name} y ahora te quiere matar");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction(){
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Que Harás?"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerSkill(){
        state = BattleState.PlayerSkill;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableSkillSelector(true);
    }

    IEnumerator PerformPlayerSkill(){
        state = BattleState.Busy;
        var skill = playerUnit.crit.Skills[currentSkill];

        yield return dialogBox.TypeDialog($"{playerUnit.crit.Base.Name} used {skill.Base.Name}");

        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnit.crit.TakeDamage(skill, playerUnit.crit);
        yield return enemyHUD.UpdateHp();

        if(isFainted){
            yield return dialogBox.TypeDialog($"{enemyUnit.crit.Base.Name} no te pudo matar");
        }else{
            StartCoroutine(EnemySkill());
        }
    }

    IEnumerator EnemySkill(){
        state = BattleState.EnemySkill;
        var skill = enemyUnit.crit.GetRandomSkill();

        yield return dialogBox.TypeDialog($"{enemyUnit.crit.Base.Name} used {skill.Base.Name}");

        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnit.crit.TakeDamage(skill, enemyUnit.crit);
        yield return playerHUD.UpdateHp();

        if(isFainted){
            yield return dialogBox.TypeDialog($"{enemyUnit.crit.Base.Name} se deshizo del insecto que lo tocó");
        }else{
            PlayerAction();
        }
    }

    private void Update(){
        if(state == BattleState.PlayerAction){
            HandleActionSelection();
        }else if(state == BattleState.PlayerSkill){
            HandleMoveSelection();
        }
    }

    void HandleActionSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentAction < 1){
                ++currentAction;
            }
        }else if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentAction > 0){
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if(Input.GetKeyDown(KeyCode.Return)){
            if(currentAction == 0){
                PlayerSkill();
            }else if(currentAction == 1){
                //Run
            }
        }
    }

    void HandleMoveSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentSkill < playerUnit.crit.Skills.Count - 1){
                ++currentSkill;
            }
        }else if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentSkill > 0){
                --currentSkill;
            }
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentSkill < playerUnit.crit.Skills.Count - 2){
                currentSkill += 2;
            }
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentSkill > 1){
                currentSkill -= 2;
            }
        }

        dialogBox.UpdateSkillSelection(currentSkill,playerUnit.crit);

        if(Input.GetKeyDown(KeyCode.Return)){
            dialogBox.EnableSkillSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerSkill());
        }

    }

}
