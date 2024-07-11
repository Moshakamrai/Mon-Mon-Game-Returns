
using UnityEngine;

[CreateAssetMenu(fileName = "RandomBurst", menuName = "Skills/RandomBurst")]
public class RandomBurst : Skill
{
    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Random Burst applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateRandomBurst();
    }
}
