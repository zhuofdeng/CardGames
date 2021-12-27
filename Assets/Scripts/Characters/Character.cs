using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    IDLE,
    ATTACK,
    DEAD,
    FROZEN,
    WALK,
    // TODO, ADD MORE IF NEEDED.
}
/*
    This is a Base class for all character types.
*/
public class Character : MonoBehaviour
{
    protected uint m_health;
    protected uint m_attack;
    protected uint m_speed;

    protected State m_characterState;

    Animator m_animator;
    int isWalkingHash;
    int isAttackingHash;
    int isDeadHash;
    int isIdleHash;

    public uint Health { get; private set; }

    public uint Attack { get; private set; }

    public uint Speed { get; private set; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_characterState = State.IDLE;
        m_animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isDeadHash = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        bool isIdle = m_animator.GetBool(isIdleHash);
        bool isAttacking = m_animator.GetBool(isAttackingHash);
        bool isWalking = m_animator.GetBool(isWalkingHash);
        bool isDead = m_animator.GetBool(isDeadHash);

        switch(m_characterState) 
        {
            case State.IDLE:
                if (!isWalking && !isAttacking) {
                    m_animator.SetBool(isWalkingHash, false);
                    m_animator.SetBool(isAttackingHash, false);
                }
            break;
            case State.ATTACK:
                if (!isAttacking) {
                    m_animator.SetBool(isAttackingHash, true);
                }
            break;
            case State.DEAD:
                if (!isDead) {
                    m_animator.SetBool(isDeadHash, true);
                }
            break;
            case State.WALK:
                if (!isWalking) {
                    m_animator.SetBool(isWalkingHash, true);
                }
            break;
        } 
    }

    // base attack, child classes should override this if they have other types of attack ie magic etc.
    virtual public void CauseDamage(Character opponent)
    {
        opponent.TakeDamage(this.Attack);
    }

    public void TakeDamage(uint attack)
    {
        this.m_health -= attack;
        if (this.m_health <= 0) {
            this.m_characterState = State.DEAD;
        }
    }
}
