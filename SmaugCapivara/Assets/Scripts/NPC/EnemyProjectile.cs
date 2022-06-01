using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem destroyParticle;
    [SerializeField] private AttackInfo attackInfo;
    [SerializeField] private float maxScreenTime;

    private void Start()
    {
        attackInfo.setAttackClass(1, AttackType.MEDIUM);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        maxScreenTime -= Time.deltaTime;
        if (maxScreenTime < 0)
            destroyBehaviour();
    }

    public void destroyBehaviour()
    {
        Instantiate(destroyParticle, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
