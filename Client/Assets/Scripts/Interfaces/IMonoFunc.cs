using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSL
{
    public interface IMonoFunc
    {
        public void Update();
        public void LateUpdate();
    }
}