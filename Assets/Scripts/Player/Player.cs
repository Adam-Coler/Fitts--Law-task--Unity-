using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFitts
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
        }
    }
}