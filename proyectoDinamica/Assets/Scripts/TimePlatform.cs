using System.Collections;
using UnityEngine;

public class TimePlatform : MonoBehaviour {

    public float timeTillFall, timeTillReset;

    private bool startFalling = false, isInOriginalPosition = true, playerCollided = false;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (playerCollided && startFalling)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - .1f, transform.position.z), Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            playerCollided = true;
            other.gameObject.GetComponentInChildren<GroundCheck>().Grounded = true;
            StartCoroutine(Fall());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.gameObject.GetComponentInChildren<GroundCheck>().Grounded = false;
            StartCoroutine(Reset());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timeTillFall);
        startFalling = true;
        isInOriginalPosition = false;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(timeTillReset);
        transform.position = originalPosition;
        playerCollided = false;
        startFalling = false;
    }
}
