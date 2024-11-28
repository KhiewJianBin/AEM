///This exist for now because of some issues with custom editor tools - like custompropertydrawer not working properly
/// https://answers.unity.com/questions/1667834/how-do-i-prevent-argument-exception-getting-contro.html

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourEditor : Editor { }
