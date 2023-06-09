using System.Collections;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This component triggers an explosion effect and destroys its object
 * whenever its object collides with something in a velocity above the threshold.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class CollisionExploder : MonoBehaviour
{
    [SerializeField]
    float minImpulseForExplosion = 1.0f;

    [SerializeField]
    GameObject explosionEffect = null;

    [SerializeField]
    float explosionEffectTime = 0.68f;

    [SerializeField]
    string wonScreen = "WonScreen";

    [SerializeField]
    string gameOverScreen = "GameOver";

    [SerializeField]
    string astroidTag = "Astroid";

    [SerializeField]
    string planetTag = "Jupiter";

    [SerializeField]
    int collisionAngle = 5;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // In 3D, the Collision object contains an .impulse field.
        // In 2D, the Collision2D object does not contain it - so we have to compute it.
        // Impulse = F * DeltaT = m * a * DeltaT = m * DeltaV
        float impulse = collision.relativeVelocity.magnitude * rb.mass;
        float angle = rb.transform.rotation.z;
        print(impulse);
        Debug.Log(
            gameObject.name
                + " collides with "
                + collision.collider.name
                + " at velocity "
                + collision.relativeVelocity
                + " [m/s], impulse "
                + impulse
                + " [kg*m/s]"
        );
        // Check if the collision is also an explosion.
        if (
            impulse > minImpulseForExplosion
            || angle > collisionAngle
            || angle < -collisionAngle
            || collision.collider.tag == astroidTag
        )
        {
            // Start the explosion
            StartCoroutine(Explosion());
        }
        else if (collision.collider.tag == planetTag)
        {
            // Start the explosion
            StartCoroutine(Explosion());
            Debug.Log(collision.collider.name);
        }
        else
        {
            // Go to the "WonScreen" scene.
            SceneManager.LoadScene(wonScreen);
        }
    }

    IEnumerator Explosion()
    {
        explosionEffect.SetActive(true);
        yield return new WaitForSeconds(explosionEffectTime);
        SceneManager.LoadScene(gameOverScreen);
    }
}
