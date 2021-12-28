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
    protected float m_awarenessDistanceSquared;

    protected float m_attackRate;

    protected State m_characterState;

    Animator m_animator;
    int isWalkingHash;
    int isAttackingHash;
    int isDeadHash;

    public uint Health { get { return m_health; } private set { m_health = value;} }

    public uint Attack { get { return m_attack; } private set { m_attack = value; } }

    public uint Speed { get { return m_speed; } private set { m_speed = value; } }

    public float AwarenessDistanceSquared { get { return m_awarenessDistanceSquared; } private set { m_awarenessDistanceSquared = value; }}

    // Start is called before the first frame update
    public virtual void Start()
    {
        // set default values.
        m_characterState = State.IDLE;
        m_attackRate = 0.5f;
        // this is 100 units squared, so it is actually 10 units away.
        // we use squared distance because square root is a expensive calculation.
        m_awarenessDistanceSquared = 100;

        m_animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isDeadHash = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        bool isAttacking = m_animator.GetBool(isAttackingHash);
        bool isWalking = m_animator.GetBool(isWalkingHash);
        bool isDead = m_animator.GetBool(isDeadHash);

        switch(m_characterState) 
        {
            case State.IDLE:
                if (isWalking || isAttacking) {
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

    public void LookAtPoint(Transform _transform) {
        // Determine which direction to rotate towards
        Vector3 targetDirection = transform.position - _transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = m_speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection * -50.0f, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // base attack, child classes should override this if they have other types of attack ie magic etc.
    virtual public void CauseDamage(Character opponent)
    {
        opponent.TakeDamage(Attack);
    }

    public void TakeDamage(uint attack)
    {
        m_health -= attack;
        if (m_health <= 0) {
            m_characterState = State.DEAD;
        }
    }
}
