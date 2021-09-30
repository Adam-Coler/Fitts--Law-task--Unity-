using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TiltFitts
{
    public class FittsTarget : MonoBehaviour
    {
        public TMP_Text text;
        public int targetNumber = -1;
        public Material isNextTargetMat;

        public delegate void OnTargetExit();
        public static event OnTargetExit onTargetExit;

        public delegate void OnTargetEnter();
        public static event OnTargetEnter onTargetEnter;

        private GameObject player;

        public bool isActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;

                if (value)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    renderer.material = new Material(isNextTargetMat);
                }
            }
        }

        private bool _isActive = false;

        private Material baseMat;
        private Renderer renderer;

        private void Awake()
        {
            if (!transform.GetChild(0).TryGetComponent<Renderer>(out renderer))
            {
                Debug.LogError("No renderer on " + this.name);
            }

            baseMat = renderer.material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isActive)
            {
                 onTargetEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (isActive)
            {
                onTargetExit();
            }
        }

        private float smallestDist = 999f;
        private void Update()
        {
            if (isActive)
            {
                Vector3 levelWithPlayer = transform.position + player.transform.localScale.y / 2 * Vector3.up;
                float distance = Vector3.Distance(levelWithPlayer, player.transform.position);

                if (distance < smallestDist)
                {
                    renderer.material.color = Color.Lerp(Color.green, Color.red, distance);
                    smallestDist = distance;
                }
            }
        }

        public void setText(string name)
        {
            if (text != null)
            {
                text.text = name;
            }
        }

    }
}
