using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject boomerangPrefab; // Prefab of the boomerang object
    public Transform boomerangSpawnPoint; // Transform representing the spawn point of the boomerang
    public float throwForce = 20f; // Force applied to the thrown boomerang
    public float returnForce = 5f; // Force applied when the boomerang returns
    public float returnDelay = 2f; // Delay in seconds before the boomerang returns
    public float maxDistance = 20f; // Maximum distance the boomerang can travel before returning

    private GameObject currentBoomerang; // Reference to the currently active boomerang
    private Rigidbody boomerangRigidbody; // Reference to the boomerang's Rigidbody component
    private Vector3 playerStartPosition; // Player's initial position when the boomerang is thrown
    private float returnTimer; // Timer for delaying the boomerang's return

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBoomerang == null)
            {
                ThrowBoomerang();
            }
            else
            {
                ReturnBoomerang();
            }
        }

        if (currentBoomerang != null)
        {
            if (returnTimer <= 0f)
            {
                if (Vector3.Distance(currentBoomerang.transform.position, transform.position) > maxDistance)
                {
                    ReturnBoomerang();
                }
            }
            else
            {
                returnTimer -= Time.deltaTime;
            }
        }
    }

    private void ThrowBoomerang()
    {
        currentBoomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, Quaternion.identity);
        boomerangRigidbody = currentBoomerang.GetComponent<Rigidbody>();
        playerStartPosition = transform.position;

        boomerangRigidbody.AddForce(boomerangSpawnPoint.forward * throwForce, ForceMode.Impulse);
    }

    private void ReturnBoomerang()
    {
        if (currentBoomerang == null)
            return;

        boomerangRigidbody.velocity = Vector3.zero;
        boomerangRigidbody.useGravity = false;

        returnTimer = returnDelay;

        Vector3 direction = (playerStartPosition - currentBoomerang.transform.position).normalized;
        boomerangRigidbody.AddForce(direction * returnForce, ForceMode.Impulse);

        Invoke("DisposeBoomerang", returnDelay);
    }

    private void DisposeBoomerang()
    {
        Destroy(currentBoomerang);
        currentBoomerang = null;
        boomerangRigidbody = null;
        returnTimer = 0f;
    }
}
