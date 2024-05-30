﻿public class CoreComp<T> where T : CoreComponent
{
    private Core core;
    private T comp;
    
    public T Comp=>comp?comp:core.GetCoreComponent(ref comp);

    public CoreComp(Core core)
    {
        this.core = core;
    }
}
