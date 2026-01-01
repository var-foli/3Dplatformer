using UnityEngine;
using UnityEngine.InputSystem;

public class StarManager : MonoBehaviour
{

    // star effect

    public ParticleSystem starEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            starEffect.Play();
        }
    }
}
