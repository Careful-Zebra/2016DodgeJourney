using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CarController : MonoBehaviour {
    #region Serialized Vars
    [SerializeField]
    float fadeOutTime;

    // [SerializeField]
    // [Tooltip("The object that holds references to the CarController scripts of all cars")]
    // private CarHolder carHolder;
    #endregion

    #region Object Vars
    private bool isDead = false;
    //private bool isStopped = false;
    #endregion

    #region Cached Vars
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Material mat;
    #endregion

    #region Not sure what to call this, private variables? Currently it just has the CarHolder - ella

    private CarHolder carHolder;

    #endregion

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        mat = GetComponent<Material>();
        carHolder = GameObject.Find("CarHolderObject").GetComponent<CarHolder>();

        //adds this carcontroller script to the set of all carcontroller scripts used for score and hud
        carHolder.AddCarToSet(this);
    }

    // Start is called before the first frame update
    void Start() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("collsion detected");
        if (isDead) return; // Prevent Re-collisions

        SoundManager.instance.CarCrashSFX();

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

    // public void Stop()
    // {
    //     isStopped = true;
    // }

    #region Enable/Disable

    void OnDestroy()
    {

        //removes this carcontroller script from the set of carcontroller scripts used for score and hud
        carHolder.RemoveCarFromSet(this);
    }

    #endregion


}
