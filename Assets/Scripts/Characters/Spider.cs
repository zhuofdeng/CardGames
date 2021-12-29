using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        m_attack = 3;
        m_health = 5;
        m_speed = 20;
        m_characterState = State.WALK;
        m_awarenessDistanceSquared = 10;
    }

    protected void CheckForNearbyHeroes() {
        Game game = Game.Instance;
        List<Hero> heros = game.getNearByHeroes(this);
        if (heros.Count > 0) {
            m_characterState = State.ATTACK;
            m_targetHero = heros[0];
            LookAtPoint(m_targetHero.transform);
        }
    }
    // Update is called once per frame
    public override void Update()
    {
        // call super...
        base.Update();

        CheckForNearbyHeroes();

        switch(m_characterState) {
            case State.WALK:
            LookAtPoint(m_currentNavPoint.transform);
            if (transform.position != m_currentNavPoint.transform.position) {
                // move toward the current nav point
                transform.position = Vector3.MoveTowards(transform.position, m_currentNavPoint.transform.position, Time.deltaTime * m_speed);
            } else {
                if (m_currentNavPoint.Index < m_navPath.navPoints.Count - 1) {
                    m_currentNavPoint = m_navPath.navPoints[m_currentNavPoint.Index + 1];
                }
            }
            break;
            case State.ATTACK:
                m_attackRate -= Time.deltaTime;
                if (m_attackRate <= 0) {
                    m_attackRate = 0.5f;
                    CauseDamage(m_targetHero);
                }
            break;

        }
    }
}
