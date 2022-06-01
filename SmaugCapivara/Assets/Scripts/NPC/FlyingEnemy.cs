using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float switchTime;
    [SerializeField] private float shootCooldown;
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject aimAnchor;
    [SerializeField] private GameObject aimPoint;
    [SerializeField] private GameObject projectile;

    private GameObject player;

    private float health;
    private Vector2 directionalInput;
    private IEnumerator verticalMovementCorroutine;
    private IEnumerator shootingCorroutine;

    void Start()
    {
        health = characterStatus.health;
        player = GameOrchestrator.Instance.getStageController().playerController.gameObject;
    }

    private void OnDisable()
    {
        if (verticalMovementCorroutine != null)
        {
            StopCoroutine(verticalMovementCorroutine);
            StopCoroutine(shootingCorroutine);
        }
    }

    void Update()
    {
        var dir = player.transform.position - aimAnchor.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        aimAnchor.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        spriteRenderer.flipX = player.transform.position.x < transform.position.x;

        if ((player.transform.position.x - transform.position.x) > 5)
        {
            rigidBody2D.AddForce(new Vector2(10, 0), ForceMode2D.Force);
        }
        else if((player.transform.position.x - transform.position.x) < -5)
        {
            rigidBody2D.AddForce(new Vector2(-10, 0), ForceMode2D.Force);
        }

        if ((player.transform.position.y - transform.position.y +4.5) > 2)
        {
            rigidBody2D.AddForce(new Vector2(0, 10), ForceMode2D.Force);
        }
        else if ((player.transform.position.y - transform.position.y +4.5) < -2)
        {
            rigidBody2D.AddForce(new Vector2(-0, -10), ForceMode2D.Force);
        }
    }

    public void resetBehaviour()
    {
        verticalMovementCorroutine = upAndDown(true);
        shootingCorroutine = shootCorroutine();
        StartCoroutine(verticalMovementCorroutine);
        StartCoroutine(shootingCorroutine);
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

    IEnumerator upAndDown(bool goingForward)
    {
        if (goingForward)
            rigidBody2D.AddForce(new Vector2(0, characterStatus.speed),ForceMode2D.Force);
        else
            rigidBody2D.AddForce(new Vector2(0, - characterStatus.speed), ForceMode2D.Force);
        yield return new WaitForSeconds(switchTime);
        verticalMovementCorroutine = upAndDown(!goingForward);
        StartCoroutine(verticalMovementCorroutine);
    }

    IEnumerator shootCorroutine()
    {
        yield return new WaitForSeconds(shootCooldown);
        Instantiate(projectile, aimPoint.transform.position, aimPoint.transform.rotation);
        shootingCorroutine = shootCorroutine();
        StartCoroutine(shootingCorroutine);
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
