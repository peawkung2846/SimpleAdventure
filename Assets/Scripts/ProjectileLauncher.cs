using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    // Start is called before the first frame update
    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab,transform.position,projectilePrefab.transform.rotation);
        Vector3 origScale =  projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            origScale.x * (transform.localScale.x > 0 ? 1 : -1),
            origScale.y,
            origScale.z
        );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
