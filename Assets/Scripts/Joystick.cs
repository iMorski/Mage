using System;
using UnityEngine;

public class Joystick : MonoBehaviour
{
   [SerializeField] private Vector2 ScreenAreaX;
   [SerializeField] private Vector2 ScreenAreaY;
   [Range(0, 1)][SerializeField] private int DragPercentage;
   
   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         
      }
      else if (Input.GetMouseButtonUp(0))
      {
         
      }
   }
}
