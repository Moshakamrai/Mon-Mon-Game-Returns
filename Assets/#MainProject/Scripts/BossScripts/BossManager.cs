using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    public List<BossBase> bosses;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DamageAllBosses(int damageAmount)
    {
        foreach (var boss in bosses)
        {
            if (boss != null)
                boss.ApplyDamage(damageAmount);
        }
    }

    public void BuffAllBosses(int buffAmount)
    {
        foreach (var boss in bosses)
        {
            if (boss != null)
                boss.Buff(buffAmount);
        }
    }

    public void DamageBoss(int index, int damageAmount)
    {
        if (index >= 0 && index < bosses.Count && bosses[index] != null)
        {
            bosses[index].ApplyDamage(damageAmount);
        }
    }

    public void BuffBoss(int index, int buffAmount)
    {
        if (index >= 0 && index < bosses.Count && bosses[index] != null)
        {
            bosses[index].Buff(buffAmount);
        }
    }

    public void DamageBossByName(string name, int damage)
{
    foreach (var boss in bosses)
    {
        if (boss != null && boss.bossName == name)
        {
            boss.ApplyDamage(damage);
            return;
        }
    }
    Debug.LogWarning($"Boss with name {name} not found.");
}

public void BuffBossByName(string name, int buffAmount)
{
    foreach (var boss in bosses)
    {
        if (boss != null && boss.bossName == name)
        {
            boss.Buff(buffAmount);
            return;
        }
    }
    Debug.LogWarning($"Boss with name {name} not found.");
}

}
