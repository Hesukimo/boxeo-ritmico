using UnityEngine;

public class Texto : MonoBehaviour
{
    float speed = 1.2f;
    float acc = 0.9f;
    Color c;

    private TextMesh mesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        speed -= acc * Time.deltaTime;
        c = mesh.color;
        c.a -= 1f * Time.deltaTime;
        mesh.color = c;
        if(speed < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
