using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamerSquid
{
    /***
     *  Class GSLight
     *  
     *  This class can be added to a game object to simulate a light (for example, normal household ceiling light).  It does this by: 
     *    1. turning on/off UnityEngine.Light(s) 
     *    2. setting the emission of a material to simulate a material with light emission
     *    
     *  To use this script:
     *    1. Place the script as a component on your game object.  Your game object might be a desk lamp etc. This game object is referred to as the 'Parent game object' below.
     *    
     *    2. Set the GSLight properties in the Unity inspector on your parent game object:
     *       a. Emissive Object List (Size) - Set to the amount of emissive game objects the parent game object has
     *         i.  Add your object that you want to emit light to the emissive object.
     *        ii.  Set the Emissive Mat Index (emission material index) property to the material on the emissive object you wish to eluminate.  
     *             For example, if a light bulb object is set to the emissive object it may have two materials. Glass (for the bulb) and metal for the screw thread. In this case,
     *             set the emission mat index to the glass. To find the index of the glass, look at the emissive object in the inspector. Under the Mesh Renderer, your materials will be listed.
     *             Use the order of the materials as the emissive material index (0 based).
     *       b. Light List (Size) - Set to the amount of UnityEngine.Light objects you want to turn on/off
     *       
     *    3. Use the public methods below to manipulate the stae of the light (on or off)
     *    
     ***/
    public class GSLight : MonoBehaviour
    {
        [SerializeField]
        private EmissiveGameObject[] _emissiveObjectList;   //This is the list of game objects which will have 'emission' turned on/off with the light

        [SerializeField]
        private Light[] _lightList;    //This is an array of Unity engine lights.  These will be turned on/off accordingly

        private bool _isOn = true;


        /***
         *  Is the light turned on
         ***/
        public bool IsOn()
        {
            return _isOn;
        }

        /***
         *  Turn the light on
         ***/
        public void TurnOn()
        {
            //Set the emission value for the emissive material
            SetEmissionColor(true);

            //Turn on the UnityEngine.Light(s)
            SetLightsActive(true);

            _isOn = true;
        }

        /***
         *  Turn the light off
         ***/
        public void TurnOff()
        {
            //Set the emission value for the emissive material
            SetEmissionColor(false);

            //Turn on the UnityEngine.Light(s)
            SetLightsActive(false);
            
            _isOn = false;
        }

        /***
         *  Toggle the light on/off
         ***/
        public void Toggle()
        {
            if (_isOn)
                TurnOff();
            else
                TurnOn();
        }

        /***
         *  Set the emission value for the emissive material
         ***/
        private void SetEmissionColor (bool on)
        {
            string msg = "";
            Color targetColor = Color.black;

            if (_emissiveObjectList != null)
            {
                //For each emissive object
                foreach (EmissiveGameObject o in _emissiveObjectList)
                {
                    //Ensure the Emissive object has been set
                    if (o.GetEmissiveObject() != null)
                    {
                        //If the light is turning on, set the target color to the 'on' color
                        if (on)
                            targetColor = o.GetOnColor();

                        //Get the renderer component from the emissive material
                        Renderer renderer = o.GetEmissiveObject().GetComponent<Renderer>();

                        if (renderer != null)
                        {
                            //Get all of the materials from the renderer
                            Material[] thisMat = o.GetEmissiveObject().GetComponent<Renderer>().materials;

                            if (thisMat != null && thisMat.Length > o.GetEmissiveMatIndex())
                            {
                                //Set the emissive values of the material
                                thisMat[o.GetEmissiveMatIndex()].EnableKeyword("_EMISSION");
                                thisMat[o.GetEmissiveMatIndex()].SetColor("_EmissionColor", targetColor);
                            }
                        }
                        else
                        {
                            //Error
                            msg = "The game object '" + gameObject.name + "' is set as the Emissive Object but the game object does not have a Mesh Renderer component. ";
                            Debug.LogWarning(msg);
                        }
                    }
                    else
                    {
                        //Error
                        msg = "The 'Emissive Object' property for the game object '" + gameObject.name + "' cannot be null. Remove the element from the Emissive Object List if not needed.";
                        Debug.LogWarning(msg);
                    }
                }
            }
        }

        /***
         *  Turn the UnityEngine.Light(s) on/off
         ***/
        private void SetLightsActive (bool active)
        {
            string msg = "";

            //If the light list has at least 1 element
            if (_lightList != null && _lightList.Length > 0)
            {
                //For each light in the list 
                foreach (Light l in _lightList)
                {
                    //If the light has been set
                    if (l != null && l.gameObject != null)
                    {
                        //Set the active value according to the light (on/off)
                        l.gameObject.SetActive(active);
                    }
                    else
                    {
                        //Error
                        msg = "The 'Light list' property on the component 'GS Light' for the game object '" + gameObject.name + "' cannot be null. Remove the element from the Light List if not needed.";
                        Debug.LogWarning(msg);
                    }
                }
            }
        }
    }

}