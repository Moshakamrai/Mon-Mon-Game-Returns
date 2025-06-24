using UnityEngine;

public class TriggerBossEvents : MonoBehaviour
{
    public void DealCustomDamageToAllBosses(int damage)
    {
        BossManager.Instance?.DamageAllBosses(damage);
    }

    public void BuffSpecificBoss(int index, int buffAmount)
    {
        BossManager.Instance?.BuffBoss(index, buffAmount);
    }
}