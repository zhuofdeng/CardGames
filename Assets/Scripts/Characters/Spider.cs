using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Character
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        m_attack = 3;
        m_health = 5;
        m_characterState = State.WALK;
    }

    // Update is called once per frame
    public override void Update()
    {
        // call super...
        base.Update();

    }
}
