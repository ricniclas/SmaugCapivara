using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectAnchor;
    [SerializeField] private Vector2 originalPosition;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float offscreenTimeLimit;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool isActive;
    private bool isOnScreen;
    private float currentOffscreenTime;

    private void Start()
    {
        isOnScreen = false;
        isActive = false;
        currentOffscreenTime = 0;
        gameObjectAnchor.transform.position = originalPosition;
        switchActiveState(false);
    }

    private void Update()
    {
        if (!isOnScreen)
        {
            currentOffscreenTime += Time.deltaTime;
            if(currentOffscreenTime >= offscreenTimeLimit)
            {
                if (isActive)
                {
                    switchActiveState(false);
                }
                isActive = false;
            }
        }
        else
        {
            if (!isActive)
            {
                switchActiveState(true);
            }
            isActive = true;
        }
    }

    private void switchActiveState(bool state)
    {
        switch (enemyType)
        {
            case EnemyType.BASIC:
                if (state == true)
                {
                    BasicEnemy basicEnemy = gameObjectAnchor.GetComponent<BasicEnemy>();
                    basicEnemy.enabled = true;
                    basicEnemy.resetBehaviour();
                    rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    gameObjectAnchor.transform.position = originalPosition;
                    rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                    gameObjectAnchor.GetComponent<BasicEnemy>().enabled = false;
                }
                break;

            case EnemyType.CRUSHER:
                if (state == true)
                {
                    Crusher crusher = gameObjectAnchor.GetComponent<Crusher>();
                    crusher.enabled = true;
                    crusher.resetBehaviour();
                }
                else
                    gameObjectAnchor.GetComponent<Crusher>().enabled = false;
                break;

            case EnemyType.FLYING:
                if (state == true)
                {
                    FlyingEnemy flyingEnemy = gameObjectAnchor.GetComponent<FlyingEnemy>();
                    flyingEnemy.enabled = true;
                    flyingEnemy.resetBehaviour();
                    rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    gameObjectAnchor.transform.position = originalPosition;
                    rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                    gameObjectAnchor.GetComponent<FlyingEnemy>().enabled = false;
                }
                break;
            default:
                break;
        }
        spriteRenderer.enabled = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.tagScreenCollider))
        {
            isOnScreen = true;
            currentOffscreenTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.tagScreenCollider))
        {
            isOnScreen = false;
        }
    }

    public enum EnemyType
    {
        BASIC,
        CRUSHER,
        FLYING
    }
}
