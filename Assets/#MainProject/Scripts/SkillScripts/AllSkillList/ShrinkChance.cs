
using UnityEngine;

[CreateAssetMenu(fileName = "ShrinkChance", menuName = "Skills/ShrinkChance")]
public class ShrinkChance : Skill
{
    public float chance = 0.2f;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Shrink Chance applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateShrinkChance(chance);
    }
}
