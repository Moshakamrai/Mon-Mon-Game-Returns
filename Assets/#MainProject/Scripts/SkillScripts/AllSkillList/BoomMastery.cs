using UnityEngine;

[CreateAssetMenu(fileName = "BoomMastery", menuName = "Skills/BoomMastery")]
public class BoomMastery : Skill
{
    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Boom Mastery applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateBoomMastery(position);
    }
}
