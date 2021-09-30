using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFitts
{
    public class TiltControl : MonoBehaviour
    {
        [Range(15f, 40f)]
        public float degreeConstrant = 20f;

        [Range(0f, 5f)]
        public float speed = 3f;

        public GameObject platform;

        // Start is called before the first frame update
        void Start()
        {
            if (platform == null)
            {
                platform = GameObject.FindGameObjectWithTag("Platform");
            }
        }

        // Update is called once per frame
        void Update()
        {
            getInputs();
        }


        private void getInputs()
        {

            float horizontal = -Input.GetAxis("Horizontal") * degreeConstrant;
            float vertical = Input.GetAxis("Vertical") * degreeConstrant;

            var newRotation = Quaternion.Euler(vertical, 0f, horizontal);
            platform.transform.rotation = Quaternion.Slerp(platform.transform.rotation, newRotation, Time.deltaTime * speed);

        }
    }
}