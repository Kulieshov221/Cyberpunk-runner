using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset; //Расстояние между игроком и камерой

    void Start()
    {
        offset = transform.position - player.position; //Расстояние между игроком и камерой
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z); //Место для перемещения камеры
        transform.position = newPosition; //Перемещение камеры в указаную позицию
    }
}
