using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PC Components/CPU")]
public class CpuSO : ScriptableObject
{
    public string cName;
    public string manufacturer;
    public float cPrice;
    public int cores;
    public int threads;
    public float botClock;
    public float topClock;
    public string socket;
    public float manProcess; //size in nanometers of the transistors
    public float l3Cache;
    public float tdp;
    public GameObject cpuModel;
    public string url;

    public void  UpdateValues(int cores, int threads, float topClock, float botClock, string socket, float manProcess, float l3Cache, float tdp)
    {
        this.cores = cores;
        this.threads = threads;
        this.topClock = topClock;
        this.botClock = botClock;
        this.socket = socket;
        this.manProcess = manProcess;
        this.l3Cache = l3Cache;
        this.tdp = tdp;
        //(6, 12, 4, 3, "any", 14, 12, 65);
    }
}
