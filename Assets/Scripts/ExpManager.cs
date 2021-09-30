using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFitts
{
    public class ExpManager : MonoBehaviour
    {
        public List<FittsTarget> targetCollection;
        int targetIndex = 0;


        private void OnEnable()
        {
            FittsTarget.onTargetExit += targetExited;
        }

        private void OnDisable()
        {
            FittsTarget.onTargetExit -= targetExited;
        }

        private void targetExited()
        {
            targetCollection[targetIndex].isNext = false;
            targetIndex++;
            targetCollection[targetIndex].isNext = true;
        }

        public void startTrial()
        {
            targetCollection[targetIndex].isNext = true;
        }

    }
}