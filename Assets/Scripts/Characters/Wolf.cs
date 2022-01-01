using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    // Start is called before the first frame update
    public override void  Start()
    {
        base.Start();
        m_type = CharacterType.GROUND;
        m_attack = 8;
        m_health = 70;
        m_speed = 30;
        m_awarenessDistanceSquared = 50;
        m_attackDistanceSquared = 20;
    }

    // Update is called once per frame
    public override void  Update()
    {
        base.Update();
    }
}
