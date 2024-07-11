using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuickRecharge", menuName = "Skills/QuickRecharge")]
public class QuickRecharge : Skill
{
    public float rechargeRateIncrease;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Quick Recharge applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateQuickRecharge(rechargeRateIncrease);
    }
}
