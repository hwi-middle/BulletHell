using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float horizontalSpeedMultiplier = 1;
    [SerializeField] private float verticalSpeedMultiplier = 1;
    private Rigidbody2D rbd;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbd.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * horizontalSpeedMultiplier, Input.GetAxisRaw("Vertical") * verticalSpeedMultiplier).normalized * speed;
    }
}
