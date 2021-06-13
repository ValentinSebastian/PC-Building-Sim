using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU_Component : PC_Component
{
    public int cores;
    public int threads;
    public float botClock;
    public float topClock;
    public string socket;
    public float manProcess; //size in nanometers of the transistors
    public float l3Cache;
    public float tdp;

    public CPU_Component(int cores, int threads, float topClock, float botClock, string socket, float manProcess, float l3Cache, float tdp)
    {
        this.cores = cores;
        this.threads = threads;
        this.topClock = topClock;
        this.botClock = botClock;
        this.socket = socket;
        this.manProcess = manProcess;
        this.l3Cache = l3Cache;
        this.tdp = tdp;
    }
}
