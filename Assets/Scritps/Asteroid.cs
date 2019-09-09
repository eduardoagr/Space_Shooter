using UnityEngine;

public class Asteroid: MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed = 20f;
    // Update is called once per frame
    [SerializeField]
    private GameObject _explosonPrefab;
    [SerializeField]
    private Spawn_Manager Spawn_Manager;

    void Start() {
        Spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }
    void Update() {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Laser") {

            Instantiate(_explosonPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject, 0.12f);
            Spawn_Manager?.SpawnRoutines();
        }
        else if (collision.name == "Player") {

            return;
        }

        
    }
}
