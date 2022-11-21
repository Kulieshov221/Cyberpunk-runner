using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 dir;
    [SerializeField] private int speed; //Поле для скорсти
    [SerializeField] private float jumpForce; //Прыжок
    [SerializeField] private float gravity; //Гравитация
    [SerializeField] private GameObject losePanel; //Панель проигрыша

    private int lineToMove = 1; //Линия по которой движемся
    public float lineDistance = 4; //Растояние между линиями

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipeController.swipeRight) //Проверка на свайп вправо
        {
            if (lineToMove < 2) //Есть ли возможность движения вправо
                lineToMove++;
        }

        if(SwipeController.swipeLeft) //Проверка на свайп влево
        {
            if (lineToMove > 0) //Есть ли возможность движения влево
                lineToMove--;
        }

        if (SwipeController.swipeUp) //Проверка на свайп вверх
        {
            if (controller.isGrounded) //Есть ли возможность прыжка
                Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void Jump()
    {
        dir.y = jumpForce; //Прыжок
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime; //Что бы персонаж не улетал в космос
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
