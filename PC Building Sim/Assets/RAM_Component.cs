using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAM_Component : PC_Component
{
    private ComponentLocation mountSlot;
    public RamSO ramSpecs;
    public void SetMountSlot(ComponentLocation _mountSlot)
    {
        mountSlot = _mountSlot;
    }
    public ComponentLocation GetMountSlot()
    {
        return mountSlot;
    }

}
