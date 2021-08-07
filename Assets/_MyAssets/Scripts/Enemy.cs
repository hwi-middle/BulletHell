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
    public float bias3_A;
    public float bias3_B;
    public float delay3;

    [Header("Heart")]
    public float bulletSpeed4;
    public int lines4;

    [Header("Star")]
    public float bulletSpeed5;
    public int lines5;

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
            StartCoroutine(ShotWindmill(iteration3, lines3, bias3_A, bias3_B + 10, delay3));
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(ShotHeart(lines4));
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

    IEnumerator ShotWindmill(int iteration, int lines, float bias1, float bias2, float delay)
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

            b += Mathf.Lerp(bias1, bias2, i / (float)iteration);
            Debug.Log(b);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ShotHeart(int lines)
    {
        for (int i = 1; i <= lines; i++)
        {
            float t = (360 * (i / (float)lines)) * Mathf.Deg2Rad;
            float x = 16 * Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t);
            float y = 13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t);

            x *= 0.1f;
            y *= 0.1f;

            Vector3 vector = new Vector3(x * Mathf.Rad2Deg, y * Mathf.Rad2Deg, 0);

            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            Vector3 pos = new Vector3(x, y, 0) * 0.05f;
            GameObject instance = Instantiate(bullet, transform.position + pos, Quaternion.Euler(0, 0, angle));

            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) / 1.590547f * bulletSpeed4;   //최댓값으로 나누어 가장 빠른 탄막이 다른 탄막의 속도와 같도록 조정
        }

        yield break;
    }
}
