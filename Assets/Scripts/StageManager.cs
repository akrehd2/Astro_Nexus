using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [System.Serializable]
    public class StageAimNexumList
    {
        public List<GameObject> aimNexumGameobjectList;

        public StageAimNexumList(List<GameObject> aimNexumGameobjectList)
        {
            this.aimNexumGameobjectList = aimNexumGameobjectList;
        }
    }

    public static StageManager instance;

    public int Stage;

    public List<GameObject> aimNexumList;
    public List<GameObject> aimNexumPartList;

    public List<StageAimNexumList> stageAimNexumList;

    private void Awake()
    {
        instance = this;
    }
}
