using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillEntry> skillEntries;

    private Dictionary<string, Skill> availableSkills;

    private void Awake()
    {
        // Convert the list to a dictionary
        availableSkills = new Dictionary<string, Skill>();
        foreach (var entry in skillEntries)
        {
            availableSkills[entry.skillName] = entry.skill;
        }
    }

    public void ChooseSkill(string skillName, GameObject player)
    {
        if (availableSkills.TryGetValue(skillName, out Skill chosenSkill))
        {
            player.GetComponent<PlayerSkills>().AddSkillPermanently(chosenSkill);
            Debug.Log("Skill " + chosenSkill.name + " chosen and applied.");
        }
        else
        {
            Debug.LogError("Skill " + skillName + " not found in available skills.");
        }
    }
}


[System.Serializable]
public class SkillEntry
{
    public string skillName;
    public Skill skill;
}