using UnityEngine;
using UnityEngine.InputSystem;
using Benjathemaker;

public class CoinManager : MonoBehaviour
{


    private float speed;
    public float Gravity = -15.0f;
    public float JumpHeight = 1.5f;
    private float verticalVelocity = 0f;
    private float terminalVelocity = 53.0f;
    public float MoveSpeed = 2.0f;
    public float SpeedChangeRate = 10.0f;
    private float targetRotation;
    private Rigidbody coinRigidbody;
    public bool Grounded = true;

    // coin effect
    public ParticleSystem coinEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move()
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
            GetComponent<SimpleGemsAnim>().enabled = true;
        }
    }

    public void JumpUp()
    {
        //TryGetComponent(out coinRigidbody);
        coinRigidbody = transform.GetComponent<Rigidbody>();
        GetComponent<SimpleGemsAnim>().enabled = false;
        //Debug.Log("Jumping up!");
        // get random direction to move coin in
        targetRotation = Random.Range(0f, 360f);
        //Debug.Log($"targetRotation: {targetRotation}");
        // Jump
        // the square root of H * -2 * G = how much velocity needed to reach desired height
        verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        //Debug.Log($"verticalVelocity: {verticalVelocity}");


        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (verticalVelocity < terminalVelocity)
        {
            verticalVelocity += Gravity * Time.deltaTime;
        }

        //Debug.Log("Moving!");
        // a reference to the coin's current horizontal velocity
        float currentHorizontalSpeed = new Vector3(coinRigidbody.linearVelocity.x, 0.0f, coinRigidbody.linearVelocity.z).magnitude;

        // creates curved result rather than a linear one giving a more organic speed change
        // note T in Lerp is clamped, so we don't need to clamp our speed
        speed = Mathf.Lerp(currentHorizontalSpeed, MoveSpeed, Time.deltaTime * SpeedChangeRate);

        // round speed to 3 decimal places
        speed = Mathf.Round(speed * 1000f) / 1000f;
        //Debug.Log($"speed: {speed}");
        Vector3 rotationDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        Vector3 horizontalDirection = new Vector3(rotationDirection.x, 0.0f, rotationDirection.z);
        //Debug.Log($"rotationDirection: {rotationDirection}");
        coinRigidbody.AddForce(rotationDirection.normalized * (speed * Time.deltaTime) 
            + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime 
            + new Vector3(rotationDirection.x, 0.0f, rotationDirection.z) * 50000 * Time.deltaTime);

    }
}
