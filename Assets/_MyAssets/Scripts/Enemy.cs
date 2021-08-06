using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float DEFAULT_ANGLE = 0;
    private Player target;
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject bullet;

    [Header("Circle")]
    public float bulletSpeed1;
    public int lines1;

    [Header("Tornado")]
    public float bulletSpeed2;
    public int lines2;
    public float delay2;


    [Header("Windmill")]
    public float bulletSpeed3;
    public int iteration3;
    public int lines3;
    public float bias;
    public float delay3;

    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance;
        //StartCoroutine(SS());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(ShotCircle(lines1, false));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(ShotTornado(lines2, delay2));
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ShotWindmill(iteration3, lines3, bias, delay3));
        }
    }

    IEnumerator ShotCircle(int lines, bool setToTarget)
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;

        float angle = DEFAULT_ANGLE;
        if (setToTarget)
        {
            angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        }

        for (int i = 0; i < lines; i++)
        {
            GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

            float angleRad = angle * Mathf.Deg2Rad;
            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * bulletSpeed1;
            angle += 360 / lines;
        }
        yield break;
    }

    IEnumerator ShotTornado(int lines, float delay)
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;

        float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;

        for (int i = 0; i < lines; i++)
        {
            GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

            float angleRad = angle * Mathf.Deg2Rad;
            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * bulletSpeed2;
            angle += 360 / lines;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ShotWindmill(int iteration, int lines, float bias, float delay)
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;

        float b = 0;
        for (int i = 0; i < iteration; i++)
        {
            float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg + b;
            for (int j = 0; j < lines; j++)
            {
                GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

                float angleRad = angle * Mathf.Deg2Rad;
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * bulletSpeed3;
                angle += 360 / lines;
            }
            b += bias;
            yield return new WaitForSeconds(delay);
        }
    }
}
