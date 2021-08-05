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
        while (true)
        {
            //ShotCircle(10);
            StartCoroutine(ShotTornado(20));
            yield return new WaitForSeconds(1f);
        }
    }

    public void ShotCircle(int n)
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;

        float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;

        for (int i = 0; i < n; i++)
        {
            GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

            float angleRad = angle * Mathf.Deg2Rad;
            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * speed;
            angle += 360 / n;
        }
    }

    IEnumerator ShotTornado(int n)
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;

        float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;

        for (int i = 0; i < n; i++)
        {
            GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

            float angleRad = angle * Mathf.Deg2Rad;
            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * speed;
            angle += 360 / n;
            yield return new WaitForSeconds(0.05f);
        }
    }
}