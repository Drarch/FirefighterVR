using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public FireType fireType;

    [SerializeField]
    public List<Flammable> flameableObjectsinRadius;

    public float heatRadius = 5.0f;
    //public float heatDisperseRate = -0.5f;
    public float heatFireRate = 1.0f;
    [SerializeField]
    private float heatRate = 0.0f;

    [SerializeField]
    private float temperature = 20.0f;
    public float Temperature
    {
        get { return temperature; }
        set
        {
            value = Mathf.Clamp(value, 20, maxTemperature);

            if (value == temperature) return;
            temperature = value;

            CheckTemperature();
        }
    }

    public float fireTemperature = 600.0f;
    public float maxTemperature = 1000.0f;
    
    [SerializeField]
    private bool isOnFire = false;
    public bool IsOnFire
    {
        get { return isOnFire; }
        set
        {
            if (value == isOnFire) return;

            isOnFire = value;
            SpreadFire(value);
            SetFireParticle(value);
        }
    }

    public List<ParticleSystem> psFire;
    

    private void Awake()
    {
        CheckTemperature();
        SetFireParticle(IsOnFire);
    }

    private void Update ()
    {
        this.Temperature += heatRate;
    }

    private void StartParticleSystem()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>(true);

        foreach (ParticleSystem p in particles)
        {
            if (p.tag == "Fire")
                psFire.Add(p);
        }
    }

    private void FindObjectInHeatRadius()
    {
        flameableObjectsinRadius.RemoveAll(x => true);

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, heatRadius, LayerMask.GetMask("Flameable"));
        foreach (Collider c in hitColliders)
        {
            Flammable item = c.GetComponentInParent<Flammable>();
            if (item != this && !flameableObjectsinRadius.Contains(item))
            {
                //UnityEditor.Undo.RecordObject(this, "Add object in heat radius");
                flameableObjectsinRadius.Add(item);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
    }

    private void SpreadFire(bool onFire)
    {
        UnityEditor.Undo.RecordObject(this, "Spread Fire");

        if (onFire)
        {
            FindObjectInHeatRadius();

            this.heatRate += heatFireRate;
            foreach(Flammable f in flameableObjectsinRadius)
            {
                UnityEditor.Undo.RecordObject(f, "Spread Fire");
                f.ChangeHeatRate(heatFireRate);
            }
        }
        else
        {
            this.heatRate -= heatFireRate;
            foreach (Flammable f in flameableObjectsinRadius)
            {
                UnityEditor.Undo.RecordObject(f, "Spread Fire");
                f.ChangeHeatRate(-heatFireRate);
            }
        }
    }

    private void CheckTemperature()
    {
        IsOnFire = Temperature >= fireTemperature;
    }

    public void ChangeHeatRate(float value)
    {
        heatRate += value;
    }

    #region Particle system control

    private void SetFireParticle(bool onFire)
    {
        if (onFire)
        {
            this.StartFire();
        }
        else
        {
            this.StopFire();
        }
    }

    public void StartFire()
    {
        foreach(ParticleSystem p in psFire)
        {
            if(!p.isPlaying)
                p.Play(true);

            if(!UnityEditor.EditorApplication.isPlaying)
            {
                var main = p.main;
                main.playOnAwake = true;
            }
        }
    }

    public void StopFire()
    {
        foreach (ParticleSystem p in psFire)
        {
            if (!p.isStopped)
                p.Stop(true);

            if (!UnityEditor.EditorApplication.isPlaying)
            {
                var main = p.main;
                main.playOnAwake = false;
            }
        }
    }

    #endregion
}
