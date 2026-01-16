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
    public int logHits = 0;

    public ParticleSystem logEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initializing log's coin to be inactive
        transform.Find("Coin Object").gameObject.SetActive(false);

        InvokeRepeating(nameof(Destination), 1, frequency);
    }

    void Destination()
    {
        // changing position based on starting position
        Xcoor = transform.position.x + Random.Range(-10, 10);
        Zcoor = transform.position.z + Random.Range(-10, 10);

        stoppingDistance = Random.Range(0.5f, 1.5f);
        destination = new Vector3(Xcoor, 0, Zcoor);
        agent.SetDestination(destination);
        agent.stoppingDistance = stoppingDistance;
    }

    public void freezeEnemy()
    {
        CancelInvoke(nameof(Destination));
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        transform.Find("ParticleLeaves").gameObject.SetActive(false);
        transform.Find("ParticleSmoke").gameObject.SetActive(false);
    }

    public void unfreezeEnemy()
    {
        InvokeRepeating(nameof(Destination), 1, frequency);
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        transform.Find("ParticleLeaves").gameObject.SetActive(true);
        transform.Find("ParticleSmoke").gameObject.SetActive(true);
    }
    public void ThrowCoinUp()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        Transform coinTransform = transform.Find("Coin Object");
        CoinManager coinScript = transform.Find("Coin Object/Coin").GetComponent<CoinManager>();

        coinTransform.position = new Vector3(coinTransform.position.x, coinTransform.position.y + 1.8f, coinTransform.position.z);
        coinTransform.gameObject.SetActive(true);

        coinScript.JumpUp();

        transform.Find("FullBody").gameObject.SetActive(false);
        logEffect.Play();
    }
}
