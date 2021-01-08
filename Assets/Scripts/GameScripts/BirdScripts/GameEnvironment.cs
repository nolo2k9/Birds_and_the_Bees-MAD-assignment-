using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/*
Singelton class
*/
public sealed class GameEnvironment : MonoBehaviour
{
    //instance of singelton
   private static GameEnvironment instance;
   private List<GameObject> checkpoints = new List<GameObject>();
  
   public List<GameObject> CheckPoints {get {return checkpoints;}}

   public static GameEnvironment Singelton
   {

       get{
           if(instance == null)
           {
               instance = new GameEnvironment();
               //fing game objectswith CheckPoint Tag
               instance.CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("CheckPoint"));
                //reorder checkpoints into acending alphabetical order
               instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList();
           } 
           return instance;

       }

   }
   
}
