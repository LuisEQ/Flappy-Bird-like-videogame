using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private PlayerBehavior _player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.right * _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2.5f, 2.5f), transform.position.z); // Optional: Clamp pipe position vertically to avoid going out of the screen
        if (transform.position.x <= -17)
        {
            Destroy(this.gameObject);
        }

    }
}
