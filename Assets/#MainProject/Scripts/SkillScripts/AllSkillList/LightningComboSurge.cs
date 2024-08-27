using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightningComboSurge", menuName = "Skills/LightningComboSurge")]
public class LightningComboSurge : Skill
{
    public float blastSizeIncrease;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Mega Blast applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateLightningComboSurge(position);
    }
}
