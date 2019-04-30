using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Transform target;

    public GameObject Gun;

    public GameObject ShootEffect;

    [Header("Attributes")]

    [Range(0, 50f)]

    public float range = 15f;

    public int Price = 50;

    public float fireRate = 10f;

    private float fireCountdown = 0f;

    [Header("Unity Setups")]

    public float rotSpeed = 10f;

    public string enemyTag = "Enemy";

    [Range(0, 1f)]

    public float gunRotSpeed = 0.3f;

    public Transform partToRotate;

    public GameObject bulletPref;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetNewTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        Gun.transform.RotateAroundLocal(new Vector3(0, 0, 1), gunRotSpeed);
        var dir = target.position - transform.position;
        var lookRot = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRot, Time.deltaTime * rotSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            if (fireRate >= 0) fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(ShootEffect, firePoint.transform.position, firePoint.transform.rotation);
        var bulletGO = (GameObject)Instantiate(bulletPref, firePoint.position, firePoint.rotation);
        var bul = bulletGO.GetComponent<BulletScript>();
        if (bul != null)
        {
            bul.Seek(target);
        }
        
    }

    GameObject LowestByHP (List<GameObject> targets)
    {
        GameObject[] result = new GameObject[targets.Count];
        for (int i = 0; i < targets.Count; i++)
        {
            int j = i;
            while (j > 0 && result[j - 1].GetComponent<Enemy>().GetHealth() > targets[i].GetComponent<Enemy>().GetHealth())
            {
                result[j] = result[j - 1];
                j--;
            }
            result[j] = targets[i];
        }
        return result[0];
    }

    public void GetNewTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject nearestTarget = null;

        var newTargets = new List<GameObject>();

        foreach (var target in targets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < range)
                newTargets.Add(target);
        }

        
        if (newTargets.Count!=0)
        nearestTarget = LowestByHP(newTargets);

        if (nearestTarget != null)
        {
            target = nearestTarget.transform;
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
