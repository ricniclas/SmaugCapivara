using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float switchTime;
    [SerializeField] private CharacterStatus characterStatus;

    private float health; 

    private Vector2 directionalInput;
    private IEnumerator movementCorroutine;

    void Start()
    {
        health = characterStatus.health;
    }

    public void resetBehaviour()
    {
        directionalInput = new Vector2(0, 0);
        movementCorroutine = backAndForward(false);
        StartCoroutine(movementCorroutine);
    }

    private void OnDisable()
    {
        if (movementCorroutine != null)
            StopCoroutine(movementCorroutine);
    }

    void Update()
    {
        rigidBody2D.velocity = new Vector2(directionalInput.x * characterStatus.speed, rigidBody2D.velocity.y);
    }

    public void receiveDamage(int damage, AttackType attackType)
    {
        health -= damage;
        if (health <= 0)
            death();
    }

    public void death()
    {
        Destroy(gameObject);
    }

    IEnumerator backAndForward(bool goingForward)
    {
        if(goingForward)
            directionalInput.x = 1f;
        else
            directionalInput.x = -1f;
        yield return new WaitForSeconds(switchTime);
        movementCorroutine = backAndForward(!goingForward);
        StartCoroutine(movementCorroutine);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.tagAttack))
        {
            AttackClass attackClass = collision.GetComponent<AttackInfo>().getAttackClass();
            receiveDamage(attackClass.damage, attackClass.attackType);
            collision.GetComponent<BasicEnemy>();
        }
    }
}

public enum MovementType
{
    BACK_AND_FORWARD,
    UNTIL_BORDER
}
