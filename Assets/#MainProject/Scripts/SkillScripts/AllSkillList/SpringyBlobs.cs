
using UnityEngine;

[CreateAssetMenu(fileName = "SpringyBlobs", menuName = "Skills/SpringyBlobs")]
public class SpringyBlobs : Skill
{
    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Springy Blobs applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateSpringyBlobs();
    }
}
