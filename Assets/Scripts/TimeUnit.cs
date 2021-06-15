using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUnit 
{
    public List<LogUnit> logUnits = new List<LogUnit>();   

    public LogUnit FindLogUnit(int uid) {
        int objectsSize = logUnits.Count; 
        for(int i = 0; i < objectsSize; i++) {
            if(logUnits[i] != null && logUnits[i].uid == uid) {
                return logUnits[i];
            }
        }   
        return null;      
    }

}