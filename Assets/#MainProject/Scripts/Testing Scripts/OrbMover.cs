using UnityEngine;

public class MagicOrbMover : MonoBehaviour
{
    public string targetTag = "ProjectlileTarget";
    public float driftDistance = 7f;       // How far it drifts left or right
    public float driftDuration = 1.5f;
    public float returnDuration = 1.5f;
    public float arcHeight = 3f;

    [SerializeField] private Transform target;
    private Vector3 startPos;
    private Vector3 driftTargetPos;
    private float timer = 0f;
    private bool drifting = true;
    private bool returning = false;

    public bool damageBoss;

    void OnEnable()
    {
        GameObject boss = GameObject.FindWithTag(targetTag);
        if (boss == null)
        {
            Debug.LogWarning($"No GameObject found with tag '{targetTag}'");
            return;
        }

        // Get any child of the boss (first one)
        if (boss.transform.childCount > 0)
        {
            target = boss.transform.GetChild(1);
        }
        startPos = transform.position;

        // Randomly choose left (-X) or right (+X) in world space
        int dir = Random.value > 0.5f ? 1 : -1;
        driftTargetPos = startPos + Vector3.right * driftDistance * dir;

        timer = 0f;
        drifting = true;
        returning = false;
    }

    void Update()
    {
        if (drifting)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / driftDuration);

            transform.position = Vector3.Lerp(startPos, driftTargetPos, t);

            if (t >= 1f)
            {
                drifting = false;
                returning = true;
                timer = 0f;
                startPos = transform.position; // Start of the return path
            }
        }
        else if (returning && target != null)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / returnDuration);

            // Midpoint with arc height (upward curve only, keep it in X-Y plane)
            Vector3 midPoint = (startPos + target.position) * 0.5f;
            midPoint += Vector3.up * arcHeight;
            midPoint.z = 0; // Flatten Z axis completely for side view

            Vector3 part1 = Vector3.Lerp(startPos, midPoint, t);
            Vector3 part2 = Vector3.Lerp(midPoint, target.position, t);
            transform.position = Vector3.Lerp(part1, part2, t);

            if (t >= 1f)
            {
                returning = false;
                if (damageBoss)
                {
                    BossManager.Instance?.DamageBossByName("Teddy", 20);
                }
                else
                {
                    BossManager.Instance?.BuffBossByName("Teddy", 40);
                }
                gameObject.SetActive(false); // Optional cleanup
            }
        }
    }
}
