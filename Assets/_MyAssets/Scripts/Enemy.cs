using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float DEFAULT_ANGLE = 0;
    private Player target;
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bullet2;

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

    [Header("Double Windmill")]
    public float bulletSpeed4;
    public int iteration4;
    public int lines4;
    public float bias4_A;
    public float bias4_B;
    public float delay4;

    [Header("Heart")]
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ShotCircle(lines1, false));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ShotTornado(lines2, delay2));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShotWindmill(iteration3, lines3, bias3_A, bias3_B, delay3));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShotDoubleWindmill(iteration4, lines4, bias4_A, bias4_B, delay4));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(ShotHeart(lines5));
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
            angle += 360 / (float)lines;
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
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ShotDoubleWindmill(int iteration, int lines, float bias1, float bias2, float delay)
    {
        Vector3 targetVector = target.transform.position - transform.position;
        Vector3 len = transform.position - target.transform.position;
        
        float b1 = 0;
        float b2 = 0;
        for (int i = 0; i < iteration; i++)
        {
            float angle1 = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg + b1;
            for (int j = 0; j < lines; j++)
            {
                GameObject instance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle1));

                float angleRad = angle1 * Mathf.Deg2Rad;
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * bulletSpeed4;
                angle1 += 360 / lines;
            }

            float angle2 = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg - b2;
            for (int j = 0; j < lines; j++)
            {
                GameObject instance = Instantiate(bullet2, transform.position, Quaternion.Euler(0, 0, angle2));

                float angleRad = angle2 * Mathf.Deg2Rad;
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * bulletSpeed4;
                angle2 -= 360 / lines;
            }
            b1 += Mathf.Lerp(bias1, bias2, i / (float)iteration);
            b2 += Mathf.Lerp(bias1, bias2, i / (float)iteration);
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

            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) / 1.590547f * bulletSpeed5;   //�ִ����� ������ ���� ���� ź���� �ٸ� ź���� �ӵ��� ������ ����
        }

        yield break;
    }
}
