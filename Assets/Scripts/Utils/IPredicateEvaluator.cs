using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeringRPG.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}