using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private struct PortalDuo
    {
        public Portal A;
        public Portal B;
    }

    private Dictionary<int, PortalDuo> portals = new Dictionary<int, PortalDuo>();
    
    void Start()
    {
        Portal[] portalsArray = FindObjectsOfType<Portal>();
        foreach (Portal portal in portalsArray)
        {
            PortalDuo duo = new PortalDuo();
            if (portals.ContainsKey(portal.Id))
            {
                duo = portals[portal.Id];
            }
            if (portal.IsA)
            {
                duo.A = portal;
            }
            else
            {
                duo.B = portal;
            }
            portals[portal.Id] = duo;
        }
    }

    public Portal GetOther(Portal portal)
    {
        PortalDuo duo = portals[portal.Id];
        if (portal.IsA)
        {
            return duo.B;
        }
        else
        {
            return duo.A;
        }
    }
}
