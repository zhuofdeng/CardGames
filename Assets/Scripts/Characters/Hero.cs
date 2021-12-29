using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_characterState = State.IDLE;
        Game.Instance.AddHero(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
