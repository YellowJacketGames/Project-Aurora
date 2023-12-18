using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script handles image effects like moving or fading in and out
//Instead of keeping this methods in one class, which we will probably need in the future in other instances
//We keep it in this namespace so it can be used by other classes without too much of a hassle

namespace ImageEffects
{

    public class Effects
    {
        float time = 0;
        public bool FadeIn(Image img, float duration)
        {
            time += Time.deltaTime; //We store the time since the transition began

            if (time <= duration) //If the transition time is higher than the duration, it means the lerp has ended and the image has faded in
            {
                //We lerp the value of the image alpha from 0 to 1
                img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(0, 1, time / duration));
                return false;
            }
            else
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1); //When the lerp is done, we set the value correctly to avoid bugs

                //We reset the check and the time since transition began
                time = 0;
                return true;
            }
        }

        public bool FadeOut(Image img, float duration)
        {
            time += Time.deltaTime; //We store the time since the transition began

            if (time <= duration) //If the transition time is higher than the duration, it means the lerp has ended and the image has faded out
            {
                //We lerp the value of the image alpha from 1 to 0
                img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(1, 0, time / duration));
                return false;
            }
            else
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0); //When the lerp is done, we set the value correctly to avoid bugs

                //We reset the check and the time since transition began
                time = 0;

                return true;

            }
        }
    }
}

