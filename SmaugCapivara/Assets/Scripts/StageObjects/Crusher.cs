using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Crusher : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private BoxCollider2D damageCollider2D;
    [SerializeField] private float upDuration;
    [SerializeField] private float downDuration;
    [SerializeField] private float idleUp;
    [SerializeField] private float idleDown;
    [SerializeField] private Transform upPosition;
    [SerializeField] private Transform downPosition;
    [SerializeField] private int damage;

    private IEnumerator movementCorroutine;


    private void Start()
    {
        damageCollider2D.GetComponent<AttackInfo>().setAttackClass(damage,AttackType.HEAVY);
        damageCollider2D.enabled = false;
    }

    public void resetBehaviour()
    {
        platform.transform.position = downPosition.position;
        movementCorroutine = movePlatform(false);
        StartCoroutine(movementCorroutine);
        damageCollider2D.enabled = false;
    }

    private void OnDisable()
    {
        if(movementCorroutine != null)
            StopCoroutine(movementCorroutine);
    }

    public IEnumerator movePlatform(bool upTrue)
    {
        damageCollider2D.enabled = false;
        if (upTrue)
        {
            yield return new WaitForSeconds(idleUp);
            damageCollider2D.enabled = true;
            float elapsedTime = 0;
            while(elapsedTime < downDuration)
            {
                platform.transform.position = Vector2.Lerp(upPosition.position, downPosition.position, (elapsedTime/downDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            CameraShaker.Instance.ShakeOnce(5f, 2f, .1f, 1f);
        }
        else
        {
            yield return new WaitForSeconds(idleDown);
            float elapsedTime = 0;
            while (elapsedTime < upDuration)
            {
                platform.transform.position = Vector2.Lerp(downPosition.position, upPosition.position, (elapsedTime / upDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        movementCorroutine = movePlatform(!upTrue);
        StartCoroutine(movementCorroutine);
    }
}
