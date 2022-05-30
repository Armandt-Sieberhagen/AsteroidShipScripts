using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public List<Mesh> Meshes;
    public MeshFilter MeshyFilter;
    public MeshRenderer MeshyRenderer;
    public GameObject Explosion;
    public EnemySpawner Spawner;
    public LayerMask ShipLayerMask;
    public float MaxLifeTime;

    void Start()
    {
        MeshyFilter.mesh = Meshes[Random.Range(0, Meshes.Count)];
    }

    private void FixedUpdate()
    {
        MaxLifeTime -= Time.deltaTime;
        if (MaxLifeTime <= 0) Explode();
    }
    

    public void Explode()
    {
        if (Explosion != null) Destroy(Instantiate(Explosion, transform.position, Explosion.transform.rotation), 3f);
        Invoke("destroyRock", 0.01f);
        Spawner.CurrentAmountOfRocks--;
        Spawner.CurrentRocksBroken++;
    }

    void destroyRock()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<ShipHealth>().DestroyPlane();
            Explode();
        }
    }
}
