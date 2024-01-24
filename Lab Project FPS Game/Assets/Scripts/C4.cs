using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{

    [SerializeField] public float radius = 5f;
    [SerializeField] public float power = 500f;

    [SerializeField] ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TriggerC4()
    {
        Vector3 position  = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (Collider thing in hitColliders)
        {
            if (thing.GetComponent<Rigidbody>())
            {
                Rigidbody rb = thing.GetComponent<Rigidbody>();
                rb.AddExplosionForce(power, position, radius, 2f, ForceMode.Impulse);
            }
        }
        explosion.Play();
    }
}
