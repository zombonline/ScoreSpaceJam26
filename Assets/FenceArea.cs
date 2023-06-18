using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceArea : MonoBehaviour
{
    [SerializeField] Transform[] seats;
    int dogsTrapped = 0;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] float alphaValueMax;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Enemy") && !collision.isTrigger)
        {
            collision.GetComponent<EnemyMovement>().enabled = false;
            collision.transform.position = seats[dogsTrapped].position;
            collision.GetComponent<Animator>().SetBool("Walking", false);
            FindObjectOfType<Score>().UpdateScore(1);
            dogsTrapped++;
        }
    }

    private void Update()
    {
        var alphaValue = Mathf.PingPong(1f * Time.deltaTime, alphaValueMax);
        var meshColor = mesh.material.color;
        mesh.material.color = new Color(meshColor.r, meshColor.g, meshColor.b, alphaValue);
    }
}
