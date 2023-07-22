using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

public class InputHistory
{
    private List<Capture> m_captures = new List<Capture>();

    public Capture lastCapture
    {
        get
        {
            if (Util.IsUnusableList(m_captures))
            {
                Debug.LogWarning("Requesting input capture, while there are no valid captures yet!!");
                return null;
            }
            return m_captures[^1];
        }
    }

    /// <summary>
    /// Adds an input capture to the input history.
    /// </summary>
    public void Add(InputPackage package, int frame)
    {
        var previousCapture = lastCapture;
        var newCapture      = new Capture(package, frame);

        if (previousCapture != null)    previousCapture.next    = newCapture;
                                        newCapture.previous     = previousCapture;

        m_captures.Add(newCapture);
    }

    /// <returns>The package that is end the end of the duration between inputs at the given frame.</returns>
    public InputPackage GetPackageCeil(int frame)
    {
        if (!ValidForCheck(frame)) return new InputPackage(); 
        for (int i = m_captures.Count - 1; i >= 0; i--)
        {
            if (frame <= m_captures[i].frame) 
                return m_captures[i].package;
        }
        return new InputPackage();
    }

    /// <returns>The package that is end the start of the duration between inputs at the given frame.</returns>
    private InputPackage GetPackageFloor(int frame)
    {
        if (!ValidForCheck(frame)) return new InputPackage(); 
        for (int i = m_captures.Count - 1; i >= 0; i--)
        {
            if (m_captures[i].frame > frame)
                continue;

            return m_captures[i].package;
        }
        return new InputPackage();
    }

    /// <returns>True if we can succesfully iterate the history with the given frame count. False if not.</returns>
    public bool ValidForCheck(int frame)
    {
        if (Util.IsUnusableList(m_captures))    return false;
        if (frame < m_captures[0].frame)        return false;
        if (frame > m_captures[^1].frame)       return false;
                                                return true;
    }

    public class Capture
    {
        public readonly InputPackage package;
        public readonly int frame;

        public Capture previous = null;
        public Capture next     = null;

        public int distanceToPrevious
        {
            get
            {
                if (previous == null) return 0;
                return frame - previous.frame;
            }
        }

        public int distanceToNext
        {
            get
            {
                if (next == null) return 0;
                return next.frame - frame;
            }
        }

        public Capture(InputPackage package, int frame)
        {
            this.package    = package;
            this.frame      = frame; 
        }
    }
}
