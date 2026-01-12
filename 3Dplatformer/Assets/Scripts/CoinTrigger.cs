using UnityEngine;
using Benjathemaker;

public class CoinTrigger : MonoBehaviour
{
    // coin effect
    public ParticleSystem coinEffect;
    public SimpleGemsAnim animScript;

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
            coinEffect.Play();
        }
        else if (collision.CompareTag("Ground"))
        {
            transform.parent.GetComponent<SimpleGemsAnim>().enabled = true;
            Destroy(transform.parent.GetComponent<Rigidbody>());
            transform.parent.GetComponent<SphereCollider>().enabled = false;
            transform.parent.Find("Collider").GetComponent<SphereCollider>().enabled = false;
        }
    }
}
