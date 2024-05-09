using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject VFX;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        OninIt();
    }

    void OninIt()
    {
        rb.velocity = transform.right * speed;
        Invoke(nameof(OnDespawn), 4f);
    }

    void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(VFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
