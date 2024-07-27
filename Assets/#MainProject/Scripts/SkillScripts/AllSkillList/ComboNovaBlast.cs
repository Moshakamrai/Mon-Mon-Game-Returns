using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboNovaBlast", menuName = "Skills/ComboNovaBlast")]
public class ComboNovaBlast : Skill
{
    

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Shrink Chance applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateComboNovaBlast(position);
    }
}
