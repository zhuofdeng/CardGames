using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Character
{
    private NavPath m_navPath; 
    private NavPoint m_currentNavPoint;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        m_attack = 3;
        m_health = 5;
        m_speed = 20;
        m_characterState = State.WALK;
        NavPointsManager navPointsManager = GameObject.FindObjectOfType<NavPointsManager>();
        if (navPointsManager != null) {
            m_navPath = navPointsManager.GetNavPath();
            m_currentNavPoint = m_navPath.navPoints[0];
        }
    }

    public void LookAtPoint(Transform _transform) {
        // Determine which direction to rotate towards
        Vector3 targetDirection = transform.position - _transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = m_speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection * -50.0f, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    // Update is called once per frame
    public override void Update()
    {
        // call super...
        base.Update();

        LookAtPoint(m_currentNavPoint.transform);
        if (transform.position != m_currentNavPoint.transform.position) {
            // move toward the current nav point
            transform.position = Vector3.MoveTowards(transform.position, m_currentNavPoint.transform.position, Time.deltaTime * m_speed);
        } else {
            if (m_currentNavPoint.Index < m_navPath.navPoints.Count - 1) {
                m_currentNavPoint = m_navPath.navPoints[m_currentNavPoint.Index + 1];
            }
        }

    }
}
