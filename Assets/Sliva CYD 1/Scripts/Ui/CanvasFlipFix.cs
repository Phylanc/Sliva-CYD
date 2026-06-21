using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class CanvasFlipFix : MonoBehaviour
    {
        private void LateUpdate()
        {
            float parentX = transform.parent.localScale.x;
            transform.localScale = new Vector3(1f / parentX, 1f, 1f);
        }
    }
}
