using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VerticalBoom", menuName = "Skills/VerticalBoom")]
public class VerticalBoom : Skill
{

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Boom Mastery applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateVerticalBoom(position);
    }
}
