using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();        
        m_type = CharacterType.GROUND;
        m_attack = 2;
        m_health = 60;
        m_speed = 30;
        m_awarenessDistanceSquared = 30;
        // up close and personal before it can attack.
        m_attackDistanceSquared = 5;
    }

    // Update is called once per frame
    public override void  Update()
    {
        base.Update();
    }
}
