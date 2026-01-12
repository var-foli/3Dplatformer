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
        // initializing log's coin to be inactive
        transform.Find("Coin Object").gameObject.SetActive(false);
        //GameObject myobject = transform.Find("CoinObject").gameObject;

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
        CancelInvoke(nameof(Destination));
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        transform.Find("ParticleLeaves").gameObject.SetActive(false);
        transform.Find("ParticleSmoke").gameObject.SetActive(false);
        //logEffect.Play();
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

        // Jump
        // the square root of H * -2 * G = how much velocity needed to reach desired height
        //_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

        // apply gravity over time if under terminal (53.0f) (multiply by delta time twice to linearly speed up over time)
        //if (_verticalVelocity < 53.0f)
        //{
        //    _verticalVelocity += Gravity * Time.deltaTime;
        //}
    }
}
