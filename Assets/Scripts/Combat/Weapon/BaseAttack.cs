using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public enum Strength { Null, Light, Heavy, Throw }
    public enum State { Null, Prepare, Perform, Finish }

    public Weapon weapon;
    public Animator animator;
    public Attack attack;
    public GameObject attackArea;
    public Strength strength;
    public State state;
    public float prepLightAttackTime = 0.3f;
    public float prepHeavyAttackTime = 0.6f;
    public float lightAttackTime = 0.2f;
    public float heavyAttackTime = 0.2f;
    public float finishLightAttackTime = 0.1f;
    public float finishHeavyAttackTime = 0.1f;

    private CharacterState _characterState;
    protected BaseCollision _collision;

    private void Awake()
    {
		animator = GetComponent<Animator> ();
        _characterState = GetComponent<CharacterState>();
        _collision = GetComponent<BaseCollision>();
    }

    public void StartLightAttack()
    {
        strength = Strength.Light;
        PrepareToLightAttack();

        if (tag == "Boss")
			;
    }

    public void StartHeavyAttack()
    {
        strength = Strength.Heavy;
        PrepareToHeavyAttack();

        if (tag == "Boss")
           	;
    }

    protected virtual void PrepareToLightAttack()
    {
        SetWeaponLocalTransform();
        strength = Strength.Light;
        state = State.Prepare;
        Invoke("PerformLightAttack", prepLightAttackTime);
    }

    protected virtual void PrepareToHeavyAttack()
    {
        SetWeaponLocalTransform();
        strength = Strength.Heavy;
        state = State.Prepare;
        Invoke("PerformHeavyAttack", prepHeavyAttackTime);
    }

    private void SetWeaponLocalTransform()
    {
        attackArea.transform.localPosition = weapon.attackOffset;
        attackArea.transform.localScale = weapon.attackSize;
    }

    protected virtual void PerformLightAttack()
    {
        state = State.Perform;
        if (_characterState.state == CharacterState.State.Attack)
            attackArea.SetActive(true);
        Invoke("FinishLightAttack", lightAttackTime);
    }

    protected virtual void PerformHeavyAttack()
    {
        state = State.Perform;
        if (_characterState.state == CharacterState.State.Attack)
            attackArea.SetActive(true);
        Invoke("FinishHeavyAttack", heavyAttackTime);
    }

    protected virtual void FinishLightAttack()
    {
        state = State.Finish;
        attackArea.SetActive(false);
        Invoke("BackToIdle", finishLightAttackTime);
    }

    protected virtual void FinishHeavyAttack()
    {
        state = State.Finish;
        attackArea.SetActive(false);
        Invoke("BackToIdle", finishHeavyAttackTime);
    }

    protected virtual void BackToIdle()
    {
        attack.FinishAttack();
        state = State.Null;
        strength = Strength.Null;
    }
}