
using UnityEngine;

[CreateAssetMenu(fileName = "DetonatorFusion", menuName = "Skills/DetonatorFusion")]
public class DetonatorFusion : Skill
{
    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Detonator Fusion applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateDetonatorFusion();
    }
}
