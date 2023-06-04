using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _ray = Physics2D.Raycast(transform.position, transform.up, 20f, _layMask);
            
            Vector2 recflactPos = Vector2.Reflect(new Vector3(_ray.point.x, _ray.point.y) - transform.position, _ray.normal);
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;

            _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            if (_angle >= minMaxAngle.x && _angle <= minMaxAngle.y)
            {

                if (useRay)
                {
                    Debug.DrawRay(transform.position, transform.up * _ray.distance * 2f, Color.green);
                    Debug.DrawRay(_ray.point, recflactPos.normalized * 4, Color.green);
                }

                if (useLine)
                {
                    if (_ray.collider.tag == "top")
                    {
                        line.positionCount = 2;
                        line.SetPosition(0, transform.position);
                        line.SetPosition(1, _ray.point);
                    }
                    else
                    {
                        line.positionCount = 3;
                        line.SetPosition(0, transform.position);
                        line.SetPosition(1, _ray.point);
                        //Debug.Log("===" + _ray.point);
                        line.SetPosition(2, _ray.point + recflactPos.normalized * 2f);
                        //Debug.Log("========" + _ray.point + recflactPos.normalized * 2f);
                    }

                }
            }
            transform.rotation = Quaternion.AngleAxis(_angle, transform.forward);
                
        }
        
    }


}
