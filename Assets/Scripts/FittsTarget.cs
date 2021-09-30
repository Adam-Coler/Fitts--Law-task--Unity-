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

        private GameObject player;

        public bool isNext
        {
            get
            {
                return _isNext;
            }
            set
            {
                _isNext = value;

                if (value)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    renderer.material = new Material(isNextTargetMat);
                }
            }
        }

        private bool _isNext = false;

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

        private void OnTriggerExit(Collider other)
        {
            if (isNext) {
                onTargetExit();
            }
        }


        /// <summary>
        /// fix dist
        /// record it
        /// add clamps
        /// .25, .75, .5
        /// </summary>
        private void Update()
        {
            if (isNext)
            {
                Vector3 levelWithPlayer = transform.position + player.transform.localScale.x / 2 * Vector3.up;
                float distance = Vector3.Distance(levelWithPlayer, player.transform.position);

                Debug.LogError(distance);

                renderer.material.color = Color.Lerp(Color.green, Color.red, distance);
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
