using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DrawWithMouse drawControl;

    public float speed = 5f;

    private Vector3 startPos;

    bool startMovement = false;

    Vector3[] positions;

    int moveIndex = 0;

    private float min_X = -2.15f;

    private float max_X = 2.15f;

    private float min_Y = -4.85f;

    private float max_Y = 4.85f;

    public GameObject vfxSuccess;

    public GameObject vfxOnDeath;

    public bool moveAble = true;

    private int ID;

    private Rigidbody2D rb;

    private void Awake()
    {
        startPos = transform.position;

        ID = GetInstanceID();

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (!moveAble) return;
        drawControl.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        if (!moveAble) return;
        drawControl.Updateline();
    }

    private void OnMouseUp()
    {
        if (!moveAble) return;
        positions = new Vector3[drawControl.line.positionCount];
        drawControl.line.GetPositions(positions);
        drawControl.ResetLine();
        startMovement = true;
        moveIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public void Success()
    {
        GameObject vfx = Instantiate(vfxSuccess, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        startMovement = false;
    }

    public void ReSetPos()
    {
        GameObject vfx = Instantiate(vfxOnDeath, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        startMovement = false;
        drawControl.ResetLine();
    }

    private void Update()
    {
        if (startMovement)
        {
            CheckPos();

            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);

            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.05f)
            {
                moveIndex++;
            }

            if (moveIndex > positions.Length - 1)
            {
                startMovement = false;
            }
        }
    }

    private void CheckPos()
    {
        if (transform.position.x < min_X)
        {
            Vector3 moveDirX = new Vector3(min_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X)
        {
            Vector3 moveDirX = new Vector3(max_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, min_Y, 0f);
            transform.position = moveDirX;
        }
    }
}