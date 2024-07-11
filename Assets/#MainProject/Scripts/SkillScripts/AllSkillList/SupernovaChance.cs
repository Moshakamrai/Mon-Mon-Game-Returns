using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SupernovaChance", menuName = "Skills/SupernovaChance")]
public class SupernovaChance : Skill
{
    public float chance = 0.2f;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Supernova Chance applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateSupernovaChance(chance);
    }
}
