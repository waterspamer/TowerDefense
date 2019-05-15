using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPooledObject
{

    public Transform target;

    public GameObject ExplosionEffect;

    public float damageOnHit = 1;

    [Range(0, 30f)] public float DamageRange;

    [SerializeField] bool DealDamageInRange;

    public float speed = 70f;

    public void OnObjectSpawn()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (DealDamageInRange)
            {
                Instantiate(ExplosionEffect, transform.position, transform.rotation);
                HitTargetsInRange();
                ObjectPooler.instance.ReturnToPool(gameObject, "Rockets");
            }
            ObjectPooler.instance.ReturnToPool(gameObject, "Bullets");
            return;
        }
        transform.LookAt(target);
        var dir = target.position - transform.position;
        var distanceOnThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceOnThisFrame)
        {
            Invoke(!DealDamageInRange ? "HitTarget" : "HitTargetsInRange", 0);
            return;
        }

        transform.Translate(dir.normalized * distanceOnThisFrame, Space.World);

        
    }

    void HitTargetsInRange()
    {
        if (ExplosionEffect != null) Instantiate(ExplosionEffect, transform.position, transform.rotation);
        var targets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var target in targets)
        {
            var tmp = Vector3.Distance(transform.position, target.transform.position);
            if (tmp <= DamageRange)
            {
                target.GetComponent<Enemy>().HealthLogic(damageOnHit * (1 - tmp / DamageRange));
            }
        }
        ObjectPooler.instance.ReturnToPool(gameObject, "Rockets");
    }

    void HitTarget()
    {
        if (ExplosionEffect != null) Instantiate(ExplosionEffect, transform.position, transform.rotation);
        target.GetComponent<Enemy>().HealthLogic(damageOnHit);
        ObjectPooler.instance.ReturnToPool(gameObject, "Bullets");
    }
}
