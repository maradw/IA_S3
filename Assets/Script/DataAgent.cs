using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class Data
{
    [Range(0f, 1f)]  
    public float value;
    public float valueMax=1;
    public float time;
    public float timeRate;
    public float timeFrameRate =0;
    public Data() { 
    
    
    }
}
public class DataAgent : MonoBehaviour
{
    public Data Energy = new Data();
    public Data Sleep = new Data();
    public Data WC = new Data();

    //Energy

    Coroutine CoroutineEnergy=null;
    public bool CantLoadEnergy { get => CoroutineEnergy == null; }

    //WC
    public bool CantLoadWC { get => CoroutineWC == null; }
    Coroutine CoroutineWC = null;

    //Sleep
    Coroutine CoroutineSleep = null;
    public bool CantLoadSleep { get => CoroutineSleep == null; }

    void Start()
    {
        
    }
    #region Energy
  IEnumerator LoadEnergyTime(float time)
    {

        while(time>0)
        {
            time--;
            Energy.value = Mathf.Lerp(Energy.value, Energy.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        Energy.value = Energy.valueMax;
        StopCoroutine(CoroutineEnergy);
        CoroutineEnergy = null;

    }
    public void LoadEnergy()
    {
        if(CoroutineEnergy==null)
            CoroutineEnergy = StartCoroutine(LoadEnergyTime(Energy.time));
    }

    public void DiscountEnergy()
    {
        if(WC.timeFrameRate > WC.timeRate)
        {
            WC.timeFrameRate = 0;
            WC.value-=0.03f;
        }
        WC.timeFrameRate += Time.deltaTime;
    }
    #endregion

    #region Sleep
    IEnumerator LoadSleepTime(float time)
    {

        while (time > 0)
        {
            time--;
            Sleep.value = Mathf.Lerp(Sleep.value, Sleep.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        Sleep.value = Sleep.valueMax;
        StopCoroutine(CoroutineWC);
        CoroutineWC = null;

    }
    public void LoadSleep()
    {
        if (CoroutineWC == null)
            CoroutineWC = StartCoroutine(LoadSleepTime(Sleep.time));
    }

    public void DiscountSleep()
    {
        if (Sleep.timeFrameRate > Sleep.timeRate)
        {
            Sleep.timeFrameRate = 0;
            Sleep.value -= 0.03f;
        }
        Sleep.timeFrameRate += Time.deltaTime;
    }
    #endregion

    #region WC
    IEnumerator LoadWCTime(float time)
    {

        while (time > 0)
        {
            time--;
            WC.value = Mathf.Lerp(WC.value, WC.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        WC.value = WC.valueMax;
        StopCoroutine(CoroutineWC);
        CoroutineWC = null;

    }
    public void LoadWC()
    {
        if (CoroutineWC == null)
            CoroutineWC = StartCoroutine(LoadSleepTime(WC.time));
    }

    public void DiscountWC()
    {
        if (WC.timeFrameRate > WC.timeRate)
        {
            WC.timeFrameRate = 0;
            WC.value -= 0.03f;
        }
        WC.timeFrameRate += Time.deltaTime;
    }
    #endregion




}
