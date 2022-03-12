using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // Start is called before the first frame update
    public override void  Start()
    {
        base.Start();
        m_type = CharacterType.AIR;
        m_attack = 3;
        m_health = 5;
        m_speed = 20;
        m_awarenessDistanceSquared = 40;
        // up close and personal before it can attack.
        m_attackDistanceSquared = 10;
    }

    // Update is called once per frame
    public override void  Update()
    {
        base.Update();
    }
}
