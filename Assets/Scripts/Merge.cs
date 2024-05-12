using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    Transform Block1;
    Transform Block2;
    public float Distance;
    public float MergeSpeed;
    public GameObject vfxMerge;
    public GameObject vfxFail;
    bool CanMerge;
    public GameObject ObjectMerge;

    Vector2 StartPos;

    public Type type;

    void Start()
    {
        ID = GetInstanceID();

        StartPos = transform.position;
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }
    public void MoveTowards()
    {
        if (CanMerge)
        {
            transform.position = Vector2.MoveTowards(Block1.position, Block2.position, MergeSpeed);
            if (Vector2.Distance(Block1.position, Block2.position) < Distance)
            {
                if (ID < Block2.gameObject.GetComponent<Merge>().ID) { return; }
                GameObject vfx = Instantiate(vfxMerge, transform.position, Quaternion.identity) as GameObject;

                Destroy(vfx, 1f);
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(Block2.gameObject);
                Destroy(Block2.gameObject);
                Destroy(gameObject);
                GameObject O = Instantiate(ObjectMerge, transform.position, Quaternion.identity);
                O.transform.SetParent(GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].transform);
/*                O.AddComponent<Rigidbody2D>();
                O.GetComponent<Rigidbody2D>().gravityScale = 0;
                O.GetComponent<Rigidbody2D>().freezeRotation = true;*/
                GameManager.Instance.CheckLevelUp();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block") && ObjectMerge != null)
        {
            if (collision.gameObject.GetComponent<Merge>().type == type)
            {
                Block1 = transform;
                Block2 = collision.transform;
                CanMerge = true;
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Rigidbody2D>());
            }
        }
    }
}