using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected NavPath m_navPath; 
    protected NavPoint m_currentNavPoint;

    protected void CheckForNearbyHeroes() {
        Game game = Game.Instance;
        List<Hero> heros = game.getNearByHeroes(this);
        if (heros.Count > 0) {
            // found our target, lock on to it.
            m_target = heros[0];
            Debug.Log("found Hero!");
            LookAtPoint(m_target.transform);
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_characterState = State.MOVE;
        NavPointsManager navPointsManager = GameObject.FindObjectOfType<NavPointsManager>();
        if (navPointsManager != null) {
            m_navPath = navPointsManager.GetNavPath();
            m_currentNavPoint = m_navPath.navPoints[0];
        }
        Game.Instance.AddEnermy(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        CheckForNearbyHeroes();

        switch(m_characterState) {
            case State.MOVE:
            if (m_target != null) {
                transform.position = Vector3.MoveTowards(transform.position, m_target.transform.position, Time.deltaTime * m_speed);
                float distanceSquared = Vector3.SqrMagnitude(m_target.transform.position - transform.position);
                if (distanceSquared <= m_attackDistanceSquared) {
                    m_characterState = State.ATTACK;
                }
            } else {
                LookAtPoint(m_currentNavPoint.transform);
                // once our target is dead, lets get back to where we intended to go!
                // storm the castle!
                if (transform.position != m_currentNavPoint.transform.position) {
                    // move toward the current nav point
                    transform.position = Vector3.MoveTowards(transform.position, m_currentNavPoint.transform.position, Time.deltaTime * m_speed);
                } else {
                    if (m_currentNavPoint.Index < m_navPath.navPoints.Count - 1) {
                        m_currentNavPoint = m_navPath.navPoints[m_currentNavPoint.Index + 1];
                    }
                }
            }
            break;
            case State.ATTACK:
                m_attackRate -= Time.deltaTime;
                if (m_attackRate <= 0) {
                    m_attackRate = 0.5f;
                    CauseDamage(m_target);
                }
            break;

        }
    }
}
