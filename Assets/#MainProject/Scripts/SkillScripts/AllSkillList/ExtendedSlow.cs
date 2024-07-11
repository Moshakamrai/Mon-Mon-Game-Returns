using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtendedSlow", menuName = "Skills/ExtendedSlow")]
public class ExtendedSlow : Skill
{
    public float timeIncrease;
    public float rechargeRateDecrease;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Extended Slow applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateExtendedSlow(timeIncrease, rechargeRateDecrease);
    }
}
