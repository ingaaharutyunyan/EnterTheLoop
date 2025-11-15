using System;
using System.Collections.Generic;
using UnityEngine;
namespace Persistence
{
    public class TargetData
    {
        public int index;
        public Vector2 savedPos;

        public TargetData(int index, Vector2 pos)
        {
            this.index = index;
            this.savedPos = pos;
        }
    }
}
