using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiniMix", menuName = "Skills/MiniMix")]
public class MiniMix : Skill
{
    public int comboThreshold = 4;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Mini Mix applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateMiniMix(comboThreshold);
    }
}
