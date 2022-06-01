using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScripts : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AttackInfo attackGameObject;
    [SerializeField] private SpriteRenderer hitboxSprite;
    private BoxCollider2D attackCollider;

    private void Start()
    {
        attackCollider = attackGameObject.GetComponent<BoxCollider2D>();
        returnToNeutral();
    }

    public void setDamage(int damage, AttackType attackType)
    {
        attackGameObject.setAttackClass(damage, attackType);
    }

    public void returnToNeutral()
    {
        deactivateAttackCollider();
        playerController.finishAttack();
    }

    public void exitHitstun()
    {
        playerController.recoverFromKnockback();
    }

    public void signalDeath()
    {
        GameOrchestrator.Instance.playerDeath();
    }

    public void activateAttackCollider()
    {
        attackCollider.enabled = true;
        hitboxSprite.color = new Color(255,0,0,50);
    }

    public void rotateHitbox(DirectionChecked directionChecked)
    {
        float resultRotation;
        switch (directionChecked)
        {
            case DirectionChecked.UP_LEFT:
                resultRotation = 135;
                break;
            case DirectionChecked.UP:
                resultRotation = 90;
                break;
            case DirectionChecked.UP_RIGHT:
                resultRotation = 45;
                break;
            case DirectionChecked.LEFT:
                resultRotation = 180;
                break;
            case DirectionChecked.NEUTRAL:
                resultRotation = 0;
                break;
            case DirectionChecked.RIGHT:
                resultRotation = 0;
                break;
            case DirectionChecked.DOWN_LEFT:
                resultRotation = 225;
                break;
            case DirectionChecked.DOWN:
                resultRotation = 270;
                break;
            case DirectionChecked.DOWN_RIGHT:
                resultRotation = 315;
                break;
            default:
                resultRotation = 0;
                break;
        }
        attackGameObject.transform.rotation = Quaternion.Euler(0,0,resultRotation);
    }

    public void deactivateAttackCollider()
    {
        attackCollider.enabled = false;
        hitboxSprite.color = new Color(255, 0, 0, 0);
    }
}
