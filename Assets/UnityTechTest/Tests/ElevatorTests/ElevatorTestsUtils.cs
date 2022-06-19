using System;
using System.Collections;
using UnityEngine;

namespace UnityTechTest.Tests.ElevatorTests
{
    public static class ElevatorTestsUtils
    {
        public static IEnumerable RunUntilTestOrTimeout(Func<bool> test, float timeout=0.015f)
        {
            var t = Time.realtimeSinceStartupAsDouble;
            while (test() && Time.realtimeSinceStartupAsDouble - t < timeout)
            {
                yield return null;
            }
        }
    }
}