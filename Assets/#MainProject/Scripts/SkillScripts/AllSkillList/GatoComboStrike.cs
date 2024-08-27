using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GatoComboStrike", menuName = "Skills/GatoComboStrike")]
public class GatoComboStrike : Skill
{
    public float blastSizeIncrease;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("GatoComboStrike applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateComboNovaBlast(position);
    }
}
