using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PRIVATE VARIABLES
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private PhysicsVariables physicsVariables;
    [SerializeField] private Grapple grapple;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AnimationScripts animationScripts;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private PhysicsMaterial2D physicsMaterial2D;
    [SerializeField] private GameObject spriteAnchor;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer mainSprite;

    private int currentHealth;

    private Rigidbody2D rigidBody2D;
    private CircleCollider2D circleCollider2D;
    private float horizontalInput;
    private float verticalInput;

    private float lastGroundedTime;
    private float lastJumpTime;
    private float lastAttackInputTime;
    private bool isJumping;
    private bool isAttacking;
    private bool isReceivingDamage;
    private bool isControllable;
    private bool isInvincible;

    private float groundAngle;
    private float angleSpeedMultiplier;

    private IEnumerator invulnerabilityTimeCoroutine;
    //private IEnumerator blinkingSprite;
    #endregion

    #region LIFETIME METHODS
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rigidBody2D.sharedMaterial = physicsMaterial2D;
        physicsMaterial2D.friction = physicsVariables.friction;
        grapple.setHookValues(characterStatus.hookDistance, characterStatus.hookMovementSpeed);
        isInvincible = false;
        isControllable = true;
    }

    private void Start()
    {
        grapple.disableHook();
        currentHealth = characterStatus.health;
    }

    private void FixedUpdate()
    {

        #region Timers
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        lastAttackInputTime -= Time.deltaTime;
        #endregion

        #region Run
        animator.SetFloat(Strings.animParamPlayerRunSpeed, Mathf.Abs(rigidBody2D.velocity.x));
        animator.SetFloat(Strings.animParamPlayerAirSpeed, rigidBody2D.velocity.y);

        float horizontalInputReceived;
        if (isControllable)
        {
            horizontalInputReceived = horizontalInput;
        }
        else
        {
            horizontalInputReceived = 0;
        }

        if(horizontalInputReceived > 0)
        {
            mainSprite.flipX = false;
        }
        else if(horizontalInputReceived < 0)
        {
            mainSprite.flipX = true;
        }
        grapple.setVerticalInput(verticalInput);

        float targetSpeed = horizontalInputReceived * characterStatus.speed;

        float speedDif = targetSpeed - rigidBody2D.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? characterStatus.acceleration : characterStatus.decceleration;

        float movement = angleSpeedMultiplier * (Mathf.Pow(Mathf.Abs(speedDif) * accelRate, characterStatus.velPower) * Mathf.Sign(speedDif));

        if(Mathf.Abs(movement) < Mathf.Abs(targetSpeed))
        {
            rigidBody2D.AddForce(targetSpeed * Vector2.right);
        }
        else
        {
            rigidBody2D.AddForce(movement * Vector2.right);
        }

        #endregion

        #region Friction

        if(lastGroundedTime > 0 && Mathf.Abs(horizontalInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rigidBody2D.velocity.x), Mathf.Abs(physicsVariables.friction));
            amount *= Mathf.Sign(rigidBody2D.velocity.x);

            rigidBody2D.AddForce(Vector2.right * - amount, ForceMode2D.Impulse);
        }

        #endregion

        #region Jump

        if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping)
            jump();


        if(isJumping && rigidBody2D.velocity.y <= 0)
        {
            isJumping = false;
        }
        #endregion

        #region Attack
        if(lastAttackInputTime > 0 && !isAttacking && isControllable)
        {
            attack();
        }
        #endregion

        #region Ground Check
        if (Physics2D.OverlapBox(groundCheckPoint.position, physicsVariables.groundCheckSize, 0, groundLayer))
        {
            lastGroundedTime = physicsVariables.jumpCoyoteTime;
            float angle;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);
            if (hit.collider != null)
            {
                angle = Mathf.Atan2(hit.normal.x, hit.normal.y) * Mathf.Rad2Deg;
                groundAngle = angle;
                spriteAnchor.transform.rotation = Quaternion.Euler(0,0, groundAngle *-1);
                animator.SetBool(Strings.animParamPlayerInTheAir, false);
            }
            else
            {
                animator.SetBool(Strings.animParamPlayerInTheAir, true);
                angle = 0f;
            }

            if(rigidBody2D.velocity.x < 0)
            {
                angle *= -1;
            }

            if (angle < 0)
                angle = 0f;

            angleSpeedMultiplier = MathUtils.Remap(angle, 0, physicsVariables.maxSlopeAngle, 1, physicsVariables.slopeSpeedMultiplier);
        }
        else
        {
            animator.SetBool(Strings.animParamPlayerInTheAir, true);
        }

        #endregion

        #region Jump Gravity
        if (rigidBody2D.velocity.y < 0)
            rigidBody2D.gravityScale = physicsVariables.gravityScale * physicsVariables.fallGravityMultiplier;
        else
            rigidBody2D.gravityScale = physicsVariables.gravityScale;
        #endregion

        #region Hook
        if (grapple.isHoldingHookInput && isAttacking)
        {
            grapple.throwHook();
        }
        #endregion

    }

    #endregion

    #region PUBLIC METHODS

    public void spawn(Vector3 position)
    {
        currentHealth = characterStatus.health;
        transform.position = position;
        rigidBody2D.velocity = new Vector2(0, 0);
    }

    private void attack()
    {
        DirectionChecked directionChecked = InputDirectionCheck.checkDirection(horizontalInput, verticalInput);
        if (directionChecked == DirectionChecked.NEUTRAL)
        {
            if (mainSprite.flipX == true)
                directionChecked = DirectionChecked.LEFT;
            else
                directionChecked = DirectionChecked.RIGHT;
        }
        animationScripts.rotateHitbox(directionChecked);
        animationScripts.setDamage(characterStatus.damage, AttackType.MEDIUM);
        isAttacking = true;
        animator.SetTrigger(Strings.animParamPlayerAttack);
    }


    public void finishAttack()
    {
        isAttacking = false;
    }

    public void recoverFromKnockback()
    {
        isControllable = true;
        invulnerabilityTimeCoroutine = invulnerabilityFrames(characterStatus.invulnerabilityTimeHeavy);
        StartCoroutine(invulnerabilityTimeCoroutine);
    }

    public void receiveKnockback()
    {
        animator.SetTrigger(Strings.animParamPlayerKnockback);
        rigidBody2D.velocity = new Vector2(0, 0);
        isControllable = false;
        isInvincible = true;
        if(mainSprite.flipX == false)
            rigidBody2D.AddForce(new Vector2(physicsVariables.knockbackAmount.x * -1, physicsVariables.knockbackAmount.y),ForceMode2D.Impulse);
        else
            rigidBody2D.AddForce(physicsVariables.knockbackAmount, ForceMode2D.Impulse);
    }

    public void jump()
    {
        if (isControllable)
        {
            Vector3 direction = Quaternion.AngleAxis(90 - groundAngle, Vector3.forward) * Vector3.right;
            rigidBody2D.AddForce(direction * characterStatus.jumpHeight, ForceMode2D.Impulse);
            lastGroundedTime = 0;
            lastJumpTime = 0;
            isJumping = true;
        }
    }

    public void receiveDamage(int damage, AttackType attackType)
    {
        if(currentHealth - damage <= 0)
        {
            currentHealth = 0;
            animator.SetTrigger(Strings.animParamPlayerDeath);
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            Time.timeScale = 0;
        }
        else
        {
            switch (attackType)
            {
                case (AttackType.LIGHT):
                    invulnerabilityTimeCoroutine = invulnerabilityFrames(characterStatus.invulnerabilityTimeLight);
                    StartCoroutine(invulnerabilityTimeCoroutine);
                    currentHealth -= damage;
                    break;
                case (AttackType.MEDIUM):
                    receiveKnockback();
                    currentHealth -= damage;
                    break;
                case (AttackType.HEAVY):
                    receiveKnockback();
                    currentHealth -= damage;
                    break;
                case (AttackType.EXTREME):
                    receiveKnockback();
                    currentHealth -= damage;
                    break;
            }
        }
    }

    public void setAnimatorUpdateMode(AnimatorUpdateMode updateMode)
    {
        animator.updateMode = updateMode;
    }

    #region InputReceivers
    public void onJump()
    {
        lastJumpTime = physicsVariables.jumpBufferTime;
    }

    public void onAttackPressed()
    {
        lastAttackInputTime = physicsVariables.attackBufferTime;
    }

    public void onAttackHeld()
    {
        if (isAttacking && isControllable)
        {
            grapple.isHoldingHookInput = true;
        }
    }

    public void onAttackRelease()
    {
        grapple.disableHook();
        grapple.isHoldingHookInput = false;
    }

    public void onReleaseJump()
    {
        if (rigidBody2D.velocity.y > 0 && isJumping && isControllable)
        {
            rigidBody2D.AddForce(Vector2.down * rigidBody2D.velocity * (1 - physicsVariables.jumpCutMultiplier), ForceMode2D.Impulse);
        }
        lastJumpTime = 0;
    }

    public void setDirectionalInputs(Vector2 values)
    {
        this.horizontalInput = values.x;
        this.verticalInput = values.y;
    }

    #endregion

    #endregion
    #region Enumerators

    public IEnumerator invulnerabilityFrames(float invulnerabilityTime)
    {
        isInvincible = true;
        StartCoroutine(invulnerabilityAnimation());
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvincible = false;
    }

    public IEnumerator invulnerabilityAnimation()
    {
        while (isInvincible)
        {
            mainSprite.enabled = !mainSprite.enabled;
            yield return new WaitForSeconds(0.05f);
        }
        mainSprite.enabled = true;
    }

    #endregion


    #region COLLISIONS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Strings.tagDeathZone))
        {
            GameOrchestrator.Instance.playerDeath();
        }
        else if (collision.gameObject.CompareTag(Strings.tagCheckpoint))
        {
            Checkpoint currentCheckpoint = collision.gameObject.GetComponent<Checkpoint>();
            if (!currentCheckpoint.alreadyPassed)
            {
                GameOrchestrator.Instance.changeStageProgressionInfo(currentCheckpoint.checkpointIndex, false);
                currentCheckpoint.changeCheckpointColor(true);
            }
        }
        else if (collision.gameObject.CompareTag(Strings.tagEnemyDamage))
        {
            if (!isInvincible)
            {
                AttackClass AttackClass = collision.GetComponent<AttackInfo>().getAttackClass();
                receiveDamage(AttackClass.damage, AttackClass.attackType);
            }
        }
        else if (collision.gameObject.CompareTag(Strings.tagEnemyProjectile))
        {
            if (!isInvincible)
            {
                AttackClass AttackClass = collision.GetComponent<AttackInfo>().getAttackClass();
                receiveDamage(AttackClass.damage, AttackClass.attackType);
                collision.GetComponent<EnemyProjectile>().destroyBehaviour();
            }
        }
    }
    #endregion
}
