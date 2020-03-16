using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    private GameObject hookedTower;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = pc.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDrag()
    {
        if (!pc.isCrashed)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            hookedTower = this.gameObject;

            float distance = Vector2.Distance(pc.transform.position, hookedTower.transform.position);

            //Gravitation toward tower
            Vector3 pullDirection = (hookedTower.transform.position - pc.transform.position).normalized;
            float newPullForce = Mathf.Clamp(pc.pullForce / distance, 20, 50);
            rb2D.AddForce(pullDirection * newPullForce);

            //Angular velocity
            rb2D.angularVelocity = -pc.rotateSpeed / distance;
        }
    }

    void OnMouseUpAsButton()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        hookedTower = null;
        rb2D.angularVelocity = 0;
    }

}
