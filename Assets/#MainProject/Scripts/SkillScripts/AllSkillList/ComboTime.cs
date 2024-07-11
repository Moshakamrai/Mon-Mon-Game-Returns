using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboTime", menuName = "Skills/ComboTime")]
public class ComboTime : Skill
{
    public float comboTimeIncrease;
    public float slowMotionTimeDecrease;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Combo Time applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateComboTime(comboTimeIncrease, slowMotionTimeDecrease);
    }
}
