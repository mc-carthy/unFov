using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour 
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float meshResolution;

    public List<Transform> visibleTargets = new List<Transform>();
    public LayerMask enemyMask;
    public LayerMask obstacleMask;
    public float delay = 0.2f;

    private void Start()
    {
        StartCoroutine(FindTargetWithDelay(delay));
    }

    private void Update()
    {
        DrawFieldOfView();
    }

    private IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll((Vector2)transform.position, viewRadius, enemyMask);

        foreach (Collider2D target in targetsInViewRadius)
        {
            Transform targetXForm = target.transform;
            Vector2 dirToTarget = (targetXForm.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, dirToTarget) < (viewAngle / 2f))
            {
                float dstToTarget = Vector2.Distance(transform.position, target.transform.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) {
                    visibleTargets.Add(targetXForm);
                }
            }
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = -transform.eulerAngles.z - (viewAngle / 2f) + (stepAngleSize * i);
            Debug.DrawLine(transform.position, transform.position + (Vector3)DirFromAngle(angle, true) * viewRadius, Color.red);
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
