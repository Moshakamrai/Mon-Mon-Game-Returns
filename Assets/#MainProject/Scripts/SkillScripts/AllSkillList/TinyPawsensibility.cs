
using UnityEngine;

[CreateAssetMenu(fileName = "ActivateTinyPawsensibility", menuName = "Skills/ActivateTinyPawsensibility")]
public class ActivateTinyPawsensibility : Skill
{
    public float chance = 0.2f;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Shrink Chance applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateTinyPawsensibility(position);
    }
}
