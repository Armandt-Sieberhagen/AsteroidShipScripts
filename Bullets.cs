using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(SphereCollider))]
public class Bullets : MonoBehaviour
{


    public Rigidbody Body;
    public GameObject Explosion;
    public LayerMask Enemies;

    [Range(0f, 1f)]
    public float Bounciness;
    public float ExplosionRange;

    public int MaxColissions;
    public float MaxLifeTime;
    public bool ExplodeOnTouch = true;

    int colissions;
    PhysicMaterial Physics_mat;
    bool Exploded;

    private void Start()
    {
        Setup();
    }

    void Setup()
    {
        Physics_mat = new PhysicMaterial();
        Physics_mat.bounciness = Bounciness;
        Physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        Physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = Physics_mat;

        Body.useGravity = false;
        Exploded = false;
    }

    private void FixedUpdate()
    {
        if (colissions > MaxColissions) explode();
        MaxLifeTime -= Time.deltaTime;
        if (MaxLifeTime <= 0) explode();
    
    }

    void explode()
    {
        if (!Exploded)
        {
            if (Explosion != null) Destroy(Instantiate(Explosion, transform.position, Explosion.transform.rotation), 3f);

            Collider[] Totalenemies = Physics.OverlapSphere(transform.position, ExplosionRange, Enemies);
            foreach (Collider item in Totalenemies)
            {
                item.GetComponent<Rock>().Explode();
            }
            Invoke("DestroyBullet", 0.01f);

            Exploded = true;
        }
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        colissions++;

        if (collision.collider.CompareTag("Enemy") && ExplodeOnTouch) explode();

    }
}
