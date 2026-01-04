using UnityEngine;
using UnityEngine.AI;

public class LogController : MonoBehaviour
{
    public NavMeshAgent agent;
    float Xcoor;
    float Zcoor;
    float stoppingDistance;
    public Vector3 destination;
    public int frequency = 7;

    public ParticleSystem logEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Destination), 1, frequency);
    }

    void Destination()
    {
        //Xcoor = Random.Range(-15, 33);
        //Zcoor = Random.Range(-8, 48);

        // changing position based on starting position
        Xcoor = transform.position.x + Random.Range(-10, 10);
        Zcoor = transform.position.z + Random.Range(-10, 10);

        stoppingDistance = Random.Range(0.5f, 1.5f);
        destination = new Vector3(Xcoor, 0, Zcoor);
        agent.SetDestination(destination);
        agent.stoppingDistance = stoppingDistance;
    }

    public void OnHitFromAbove()
    {
        transform.Find("ParticleLeaves").gameObject.SetActive(false);
        transform.Find("ParticleSmoke").gameObject.SetActive(false);
        transform.Find("FullBody").gameObject.SetActive(false);
        logEffect.Play();
    }
}
