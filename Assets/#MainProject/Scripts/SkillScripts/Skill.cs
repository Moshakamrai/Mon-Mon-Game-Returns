using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public string description;
    public bool activationSkill;
    public int skillLevel;

    public abstract void ApplySkill(GameObject player, Vector3 position);
}
