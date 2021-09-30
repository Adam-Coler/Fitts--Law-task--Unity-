using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFitts
{
    public class Setup : MonoBehaviour
    {

        private GameObject target;
        private GameObject platform;
        private GameObject player;
        private List<FittsTarget> targetCollection = new List<FittsTarget>();

        /// <summary>
        /// The number of targets used
        /// </summary>
        public int targets = 12;
        
        /// <summary>
        /// W = scaling factor for the targets
        /// </summary>
        [Range(.1f, 1f)]
        public float W = .75f;

        /// <summary>
        /// D = Diameter of the targets ring
        /// </summary>
        public float D = 12f;

        public ExpManager expManager;

        private void Awake()
        {
            if (target == null)
            {
                target = (GameObject)Resources.Load("Target");
            }
            if (platform == null)
            {
                platform = (GameObject)Resources.Load("Platform");
            }
            if (player == null)
            {
                player = (GameObject)Resources.Load("Player");
            }

            if (expManager == null)
            {
                expManager = gameObject.AddComponent<ExpManager>();
            }

            setPlatform();
            setTargets(getTargetPositions());
            setPlayer();

            expManager.targetCollection = targetCollection;
            expManager.startTrial();
        }

        private void setPlayer()
        {
            Rigidbody rigidbody;

            player = Instantiate(player);
            rigidbody = player.AddComponent<Rigidbody>();
            player.transform.parent = this.transform;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.drag = 1.25f;
            rigidbody.WakeUp();
            rigidbody.AddForce(Vector3.one);
        }

        private void setPlatform()
        {
            float padding = getScale();
            padding += padding * 2;
            platform = Instantiate(platform, transform);
            platform.transform.localScale = new Vector3(D+padding, 1, D+padding);
        }

        private void setTargets(List<Vector3> targetPositions)
        {
            float scale = getScale();

            for (int i = 0; i < targetPositions.Count; i++)
            {

                Vector3 pos = targetPositions[i];
                GameObject tmp = Instantiate(target, transform);
                tmp.name = "Target " + (i + 1);
                tmp.transform.position = pos;
                tmp.transform.localScale = new Vector3(scale, 1, scale);

                FittsTarget fittsTarget;
                if (tmp.TryGetComponent<FittsTarget>(out fittsTarget))
                {
                    fittsTarget.setText((i + 1).ToString());
                    fittsTarget.targetNumber = (i + 1);
                    targetCollection.Add(fittsTarget);
                }
            }

        }

        private float getScale()
        {
            float angle = (360f / targets);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.right;
            Vector3 positionOne = transform.position + direction * D/2;

            angle = 2 * (360f / targets);
            direction = Quaternion.Euler(0, angle, 0) * Vector3.right;
            Vector3 positionTwo = transform.position + direction * D/2;

            float output = Vector3.Distance(positionOne, positionTwo);

            output = output * W;

            return output;
        }


        private List<Vector3> getTargetPositions()
        {
            List<Vector3> positions = new List<Vector3>();
            int target;
            int count = (int)Mathf.Ceil(targets % 2);

            for (int i = 0; i < targets; i++)
            {
                if (i % 2 > 0)
                {
                    target = (int)Mathf.Ceil(targets / 2) + count;
                    count += 1;
                }
                else
                {
                    target = (int)Mathf.Floor(i / 2);
                }

                float angle = target * (360f / targets);
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.right;
                Vector3 position = transform.position + direction * D/2;
                position = position + 0.0125f * Vector3.up;
                positions.Add(position);
            }

            return positions;
        }
    }
}