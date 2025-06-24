using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{
    public BossBlobLauncher blobLauncher;

    public override void UpdateBossUI()
    {
        base.UpdateBossUI();

        if (bossHealth <= 0 && blobLauncher != null)
        {
            blobLauncher.DisableLaunch();
        }
    }
}
