using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    // Start is called before the first frame update
    public override void  Start()
    {
        base.Start();
        m_type = CharacterType.GROUND;
        m_attack = 10;
        m_health = 100;
        m_speed = 5;
        m_awarenessDistanceSquared = 30;
        // up close and personal before it can attack.
        m_attackDistanceSquared = 10;
    }

    // Update is called once per frame
    public override void  Update()
    {
        base.Update();
    }
}
