using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make this show up in the editor.
[System.Serializable]
public class NavPoint {
    private int index;
    public Transform transform;

    public int Index { get; set; }
}

// Make this show up in the editor.
[System.Serializable]
public class NavPath {
    public string pathID;
    public List<NavPoint> navPoints = new List<NavPoint>();
}

public class NavPointsManager : MonoBehaviour
{
    public List<NavPath> m_navPaths = new List<NavPath>();

    void Start() {
        foreach(NavPath path in m_navPaths) {
            int index = 0;
            foreach(NavPoint navPoint in path.navPoints) {
                navPoint.Index = index;
                index++;
            }
        }
    }
    public NavPath GetNavPath(bool randomPath = false) {
        int randomIndex = Random.Range(0, m_navPaths.Count);
        return m_navPaths[randomIndex];
    }
}
