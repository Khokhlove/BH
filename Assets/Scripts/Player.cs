using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private float speed = 5;
    [SyncVar]
    [SerializeField]
    private float pushPower = 10;
    [SyncVar]
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private CollisionChecker collisionChecker;
    private float mouseY;
    private Vector3 movement;
    private Coroutine coroutine;
    [SerializeField]
    public Text scoreText;
    [SerializeField]
    private GameObject cameraPosition;

    private void Start()
    {
        if (isLocalPlayer)
        {
            Transform cameraTransform = Camera.main.gameObject.transform;
            cameraTransform.parent = cameraPosition.transform;  
            cameraTransform.position = cameraPosition.transform.position;  
            cameraTransform.rotation = cameraPosition.transform.rotation;
        }
        ScoreControllerNetwork.GetInstance().AddPlayerScore(this);
        scoreText.gameObject.SetActive(isLocalPlayer);
    }
    private void FixedUpdate()
    {
        if (isLocalPlayer == false)
            return;
        InputKey();
    }
    private void Update()
    {
        if (isLocalPlayer == false)
            return;
        InputMouse();
    }
    private void InputKey()
    {
        movement = GetDirection();
        Move(movement);
    }
    private Vector3 GetDirection()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        return new Vector3(movementX, 0, movementZ);
    }
    private void InputMouse()
    {
        mouseY =- Input.GetAxis("Mouse X"); 
        Rotate(mouseY);
        if (Input.GetMouseButtonDown(0) && coroutine == null)
            coroutine = StartCoroutine(Rush());
    }
    private void Move(Vector3 direction)
    {
        transform.Translate(direction * Time.fixedDeltaTime * speed);
    }
    private void Rotate(float angle)
    {
        gameObject.transform.rotation *= Quaternion.Euler(0, angle * rotationSpeed, 0);
    }
    private IEnumerator Rush()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(GetDirection() * pushPower, ForceMode.Impulse);
        Collider collider = collisionChecker.GetComponent<Collider>();
        collider.enabled = true;
        yield return new WaitForSeconds(1);
        collider.enabled = false;
        coroutine = null;
    }

}
