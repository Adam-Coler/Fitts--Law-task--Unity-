using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFitts
{
    public class ExpManager : MonoBehaviour
    {
        public List<FittsTarget> targetCollection;
        int currentTargetIndex = 0;


        private void OnEnable()
        {
            FittsTarget.onTargetExit += targetExited;
            FittsTarget.onTargetEnter += targetEntered;
        }

        private void OnDisable()
        {
            FittsTarget.onTargetExit -= targetExited;
            FittsTarget.onTargetEnter -= targetEntered;
        }

        private void targetEntered()
        {
            if (hasNextTarget())
            {
                currentTargetIndex++;
                targetCollection[currentTargetIndex].isActive = true;
            } else
            {
                //end
            }
        }

        private void targetExited()
        {
            if (hasPreviousTarget())
            {
                targetCollection[currentTargetIndex - 1].isActive = false;
            }
        }

        public void startTrial()
        {
            targetCollection[0].isActive = true;
        }

        private bool hasPreviousTarget()
        {
            if (currentTargetIndex >= 1) { return true; }
            return false;
        }

        private bool hasNextTarget()
        {
            if (currentTargetIndex < targetCollection.Count - 1) { return true; }
            return false;
        }
    }
}