using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State {
    IDLE,
    ATTACK,
    DEAD,
    FROZEN,
    // TODO, ADD MORE IF NEEDED.
}
/*
    This is a Base class for all character types.
*/
public class Character : MonoBehaviour
{
    protected uint health;
    protected uint attack;

    private State characterState;

    public uint Health { get; private set; }

    public uint Attack { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.characterState = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // base attack, child classes should override this if they have other types of attack ie magic etc.
    virtual public void CauseDamage(Character opponent)
    {
        opponent.TakeDamage(this.Attack);
    }

    public void TakeDamage(uint attack)
    {
        this.health -= attack;
        if (this.health <= 0) {
            this.characterState = State.DEAD;
        }
    }
}
