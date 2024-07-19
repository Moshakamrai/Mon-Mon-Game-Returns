using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HorizontalBoom", menuName = "Skills/HorizontalBoom")]
public class HorizontalBoom : Skill
{
    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Horizontal Boom applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateHorizontalBoom(position);
    }
}
