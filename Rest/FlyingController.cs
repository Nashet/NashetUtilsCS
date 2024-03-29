using UnityEngine;
using System.Collections;

namespace InterativeErosionProject
{
    public class FlyingController : MonoBehaviour
    {
        private const int mouseWheelMultiplier = 20;
        public float m_speed = 5.0f;

        void Start()
        {

        }

        void Update()
        {
            //Very simple method of moving player left, right, forward, back, up or down

            Vector3 move = new Vector3(0, 0, 0);

            //move left
            if (Input.GetKey(KeyCode.A))
                move = new Vector3(-1, 0, 0) * Time.deltaTime * m_speed;

            //move right
            if (Input.GetKey(KeyCode.D))
                move = new Vector3(1, 0, 0) * Time.deltaTime * m_speed;

            //move forward
            if (Input.GetKey(KeyCode.W))
                move = new Vector3(0, 0, 1) * Time.deltaTime * m_speed;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                move = new Vector3(0, 0, 1) * Time.deltaTime * m_speed * mouseWheelMultiplier;

            //move back
            if (Input.GetKey(KeyCode.S))
                move = new Vector3(0, 0, -1) * Time.deltaTime * m_speed;

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                move = new Vector3(0, 0, -1) * Time.deltaTime * m_speed * 20;

            //move up
            if (Input.GetKey(KeyCode.E))
                move = new Vector3(0, 1, 0) * Time.deltaTime * m_speed;

            //move down
            if (Input.GetKey(KeyCode.Q))
                move = new Vector3(0, -1, 0) * Time.deltaTime * m_speed;

            transform.Translate(move);
            //transform.position += move;

        }
    }
}
