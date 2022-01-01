using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    IDLE,
    ATTACK,
    DEAD,
    FROZEN,
    MOVE,
    // TODO, ADD MORE IF NEEDED.
}

public enum CharacterType {
    GROUND,
    AIR,
}
/*
    This is a Base class for all character types.
*/
public class Character : MonoBehaviour
{
    protected uint m_health;
    protected uint m_attack;
    protected uint m_speed;
    protected CharacterType m_type;

    protected float m_awarenessDistanceSquared;
    protected float m_attackDistanceSquared;

    protected float m_attackRate;

    protected State m_characterState;
    protected Character m_target;

    Animator m_animator;
    int isMovingHash;
    int isAttackingHash;
    int isDeadHash;

    public uint Health { get { return m_health; } private set { m_health = value;} }

    public uint Attack { get { return m_attack; } private set { m_attack = value; } }

    public uint Speed { get { return m_speed; } private set { m_speed = value; } }

    public float AwarenessDistanceSquared { get { return m_awarenessDistanceSquared; } private set { m_awarenessDistanceSquared = value; } }

    public CharacterType Type { get { return m_type; } private set {m_type = value; } }

    public float AttackDistance { get { return m_attackDistanceSquared; } private set { m_attackDistanceSquared = value; } }

    // Start is called before the first frame update
    public virtual void Start()
    {
        // set default values.
        m_characterState = State.IDLE;
        m_attackRate = 0.5f;
        // this is 100 units squared, so it is actually 10 units away.
        // we use squared distance because square root is a expensive calculation.
        m_awarenessDistanceSquared = 300;

        m_animator = GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isDeadHash = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        bool isAttacking = m_animator.GetBool(isAttackingHash);
        bool isWalking = m_animator.GetBool(isMovingHash);
        bool isDead = m_animator.GetBool(isDeadHash);

        switch(m_characterState) 
        {
            case State.IDLE:
                if (isWalking || isAttacking) {
                    m_animator.SetBool(isMovingHash, false);
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
                if (m_target != null) {
                    // release the target once dead.
                    m_target = null;
                }
            break;
            case State.MOVE:
                if (!isWalking) {
                    m_animator.SetBool(isMovingHash, true);
                }
            break;
        } 
    }

    public void LookAtPoint(Transform targetTransform) {
        // Determine which direction to rotate towards
        Vector3 targetDirection = targetTransform.position - transform.position;

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
