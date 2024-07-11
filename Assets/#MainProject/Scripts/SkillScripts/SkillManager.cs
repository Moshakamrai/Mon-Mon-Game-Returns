using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> availableSkills;

    public void ChooseSkill(int skillIndex, GameObject player)
    {
        if (skillIndex >= 0 && skillIndex < availableSkills.Count)
        {
            Skill chosenSkill = availableSkills[skillIndex];
            player.GetComponent<PlayerSkills>().AddSkillPermanently(chosenSkill);
            Debug.Log("Skill " + chosenSkill.name + " chosen and applied.");
        }
        else
        {
            Debug.Log("Invalid skill index");
        }
    }
}
