namespace VRTK.Examples
{
    using UnityEngine;

    public class FireExtinguisher_Sprayer_VR : VRTK_InteractableObject
    {
        public FireExtinguisher_Base_VR baseCan;
        public float breakDistance = 0.12f;
        public float maxSprayPower = 5f;

        public FireType fireType;
        public FireTypeFloatDictionary extinguishRateDictionary;

        private GameObject waterSpray;
        private ParticleSystem particles;
        private Collider extinguishArea;

        public void Spray(float power)
        {
            if (power <= 0)
            {
                particles.Stop();
                extinguishArea.enabled = false;
            }

            if (power > 0)
            {
                if (particles.isPaused || particles.isStopped)
                {
                    particles.Play();
                    extinguishArea.enabled = true;
                }

#if UNITY_5_5_OR_NEWER
                var mainModule = particles.main;
                mainModule.startSpeedMultiplier = maxSprayPower * power;
#else
                particles.startSpeed = maxSprayPower * power;
#endif
            }
        }

        protected override void Awake()
        {
            base.Awake();
            //waterSpray = transform.Find("WaterSpray").gameObject;
            waterSpray = transform.Find("PS_FireExtinguisher").gameObject;
            particles = waterSpray.GetComponent<ParticleSystem>();
            particles.Stop();

            extinguishArea = transform.Find("ExtinguishArea").gameObject.GetComponent<Collider>();
            extinguishArea.enabled = false;
        }

        protected override void Update()
        {
            base.Update();
            if (Vector3.Distance(transform.position, baseCan.transform.position) > breakDistance)
            {
                ForceStopInteracting();
            }
        }
    }
}