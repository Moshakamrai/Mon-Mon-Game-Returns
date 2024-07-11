using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FutureSight", menuName = "Skills/FutureSight")]
public class FutureSight : Skill
{
    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Future Sight applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateFutureSight();
    }
}
