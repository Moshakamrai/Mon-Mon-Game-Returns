using UnityEngine;

[CreateAssetMenu(fileName = "FurnovaBlast", menuName = "Skills/FurnovaBlast")]
public class FurNovaBlast : Skill
{
    public float chance = 0.2f;

    public override void ApplySkill(GameObject player, Vector3 position)
    {
        Debug.Log("Shrink Chance applied at " + position);
        player.GetComponent<PlayerSkills>().ActivateFurNovaBlast(position);
    }
}
