using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player target;
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance;
        StartCoroutine(SS());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SS()
    {
        while(true)
        {
            ShotCircle();
            yield return new WaitForSeconds(1f);
        }
    }

    public void ShotCircle()
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;

        float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
        instance.GetComponent<Rigidbody2D>().velocity = new Vector2(targetVector.x, targetVector.y).normalized * speed;
        Debug.LogError("dd");
    }
}
