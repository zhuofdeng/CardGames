using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected NavPath m_navPath; 
    protected NavPoint m_currentNavPoint;

    protected Hero m_targetHero;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
    }
}
