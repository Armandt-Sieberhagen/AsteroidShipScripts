using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    public GameObject Explosion;
    public MeshRenderer ShipRenderer;
    public void DestroyPlane()
    {
        if (Explosion != null) Destroy(Instantiate(Explosion, transform.position, Explosion.transform.rotation), 3f);
        Debug.Log("dead");
        this.gameObject.SetActive(false);
        Invoke("Respawn", 3f);
  
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restarted");
    }
}
