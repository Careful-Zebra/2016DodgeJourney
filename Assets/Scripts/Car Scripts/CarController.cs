using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CarController : MonoBehaviour {
    #region Serialized Vars
    [SerializeField]
    float fadeOutTime;
    #endregion

    #region Object Vars
    private bool isDead = false;
    #endregion

    #region Cached Vars
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Material mat;
    #endregion

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        mat = GetComponent<Material>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("collsion detected");
        if (isDead) return; // Prevent Re-collisions
        Destroy(GetComponent<ManualDrive>()); // Stops all movements
        gameObject.layer = LayerMask.NameToLayer("TrafficObjects");
        rb.velocity = Vector3.zero;
        isDead = true;
        StartCoroutine(Die());
    }


    private IEnumerator Die() {
        float t = 0f;
        while (t < fadeOutTime) {
            // Fading out a car by changing its opacity value (alpha)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(1f, 0f, fadeOutTime / t)); // replace with mat.color when textured
            t += Time.deltaTime;
            yield return null;
        }
        // Finally, delete the car
        Destroy(gameObject);
    }

    #region A*
    private void aStar()
    {
        ArrayList visited = new ArrayList();



    }
    #endregion
}
