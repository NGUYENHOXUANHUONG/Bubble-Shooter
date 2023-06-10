using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BallSpawner : MonoBehaviour
{
    private RaycastHit2D _ray;
    [SerializeField] private LayerMask _layMask;
    private float _angle;
    [SerializeField] Vector2 minMaxAngle;

    [SerializeField] bool useRay;
    [SerializeField] bool useLine;
    [SerializeField] LineRenderer line;
    List<Vector3> rayList = new List<Vector3>();

    public GameObject ball;
    public float ballSpeed = 10f;
    private bool isBallMoving = true;
    private Vector3 nextPos;

    public void FixedUpdate()
    {
        //Debug.Log(isBallMoving);

        if (!isBallMoving) return;
        Debug.Log("====" + isBallMoving);
        rayList = new List<Vector3>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (pos - transform.position).normalized;
        rayList.Add(transform.position);
        _ray = Physics2D.Raycast(transform.position, dir, 20f);

        _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        if (_angle >= minMaxAngle.x && _angle <= minMaxAngle.y)
        {
            if (_ray.collider.tag == "top")
            {
                line.positionCount = 2;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, _ray.point);
                rayList.Add(_ray.point);
            }
            if (_ray.collider.tag == "Boundary")
            {
                rayList.Add(_ray.point);
                Vector2 recflactPos = Vector2.Reflect(dir, Vector2.right);

                if (_ray.collider.name == "right")
                {
                    Vector2 moveleft = (Vector2)_ray.point - new Vector2(0.01f, 0);
                    _ray = Physics2D.Raycast(moveleft, recflactPos, 20f);
                }
                if (_ray.collider.name == "left")
                {
                    Vector2 moveright = (Vector2)_ray.point + new Vector2(0.01f, 0);
                    _ray = Physics2D.Raycast(moveright, recflactPos, 20f);
                }

                rayList.Add(_ray.point);
                line.positionCount = 3;
                line.SetPosition(0, rayList[0]);
                line.SetPosition(1, rayList[1]);
                line.SetPosition(2, rayList[2]);

            }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    BallMove();
            //}

        }

        transform.rotation = Quaternion.AngleAxis(_angle, transform.forward);
    }


    void BallMove()
    {
        isBallMoving = false;
        List<Vector3> iList = rayList;

        GameObject obj = Instantiate(ball);
        obj.transform.position = transform.position;

        if (iList.Count < 3)
        {
            obj.transform.DOMove(iList[1], 1f).OnComplete(() =>
            {
                isBallMoving = true;
            });
        }
        else
        {
            obj.transform.DOMove(iList[1], 1f).OnComplete(() =>
            {
                obj.transform.DOMove(iList[2], 1f).OnComplete(() =>
                {
                    isBallMoving = true;
                });
            });
        }
    }
    void MoveBall()
    {
        bool isMovenext = false;

        if (!isBallMoving)
        {
            return;
        }
        List<Vector3> moveList = rayList;

        ball.transform.position = Vector3.MoveTowards(ball.transform.position, nextPos, ballSpeed * Time.deltaTime);

        if (moveList.Count < 3)
        {
            Debug.Log(moveList[0] + "====" + moveList.Count);

            if (ball.transform.position == moveList[1])
            {
                isBallMoving = false;
            }
        }
        else
        {
            if (ball.transform.position == moveList[2])
            {
                isBallMoving = false;
            }
            if (ball.transform.position == moveList[1])
            {
                if (isMovenext)
                {
                    return;
                }
                nextPos = rayList[2];
                isMovenext = true;
            }
        }


    }

    void RayCastHit()
    {
        rayList = new List<Vector3>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (pos - transform.position).normalized;
        rayList.Add(transform.position);
        _ray = Physics2D.Raycast(transform.position, dir, 20f);

        _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        if (_angle >= minMaxAngle.x && _angle <= minMaxAngle.y)
        {
            if (_ray.collider.tag == "top")
            {
                line.positionCount = 2;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, _ray.point);
                rayList.Add(_ray.point);
            }
            if (_ray.collider.tag == "Boundary")
            {
                rayList.Add(_ray.point);
                Vector2 recflactPos = Vector2.Reflect(dir, Vector2.right);

                if (_ray.collider.name == "right")
                {
                    Vector2 moveleft = (Vector2)_ray.point - new Vector2(0.01f, 0);
                    _ray = Physics2D.Raycast(moveleft, recflactPos, 20f);
                }
                if (_ray.collider.name == "left")
                {
                    Vector2 moveright = (Vector2)_ray.point + new Vector2(0.01f, 0);
                    _ray = Physics2D.Raycast(moveright, recflactPos, 20f);
                }

                rayList.Add(_ray.point);
                line.positionCount = 3;
                line.SetPosition(0, rayList[0]);
                line.SetPosition(1, rayList[1]);
                line.SetPosition(2, rayList[2]);

            }
        }
    }

}
