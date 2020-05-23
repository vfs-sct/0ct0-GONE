using System.Collections;
using System.Collections.Generic;
using UnityEngine.LowLevel;
using UnityEngine;
using System.Text;



public class test : MonoBehaviour
{
[SerializeField] private static bool ShowPlayerLoop = false;




[RuntimeInitializeOnLoadMethod]
private static void AppStart()
{
    if (ShowPlayerLoop)
    {
        var def = PlayerLoop.GetDefaultPlayerLoop();
        var sb = new StringBuilder();
        RecursivePlayerLoopPrint(def, sb, 0);
        Debug.Log(sb.ToString());
    }
    
}


private static void RecursivePlayerLoopPrint(PlayerLoopSystem def, StringBuilder sb, int depth)
{
    if (depth == 0)
    {
        sb.AppendLine("ROOT NODE");
    }
    else if (def.type != null)
    {
        for (int i = 0; i < depth; i++)
        {
            sb.Append("\t");
        }
        sb.AppendLine(def.type.Name);
    }
    if (def.subSystemList != null)
    {
        depth++;
        foreach (var s in def.subSystemList)
        {
            RecursivePlayerLoopPrint(s, sb, depth);
        }
        depth--;
    }
}
}
