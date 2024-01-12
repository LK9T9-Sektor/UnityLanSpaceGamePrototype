using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Particle
{
    public class ThrusterBehaviour : NetworkBehaviour
    {
        [SyncVar]
        float thrusting;
        [SyncVar]
        float spinning;

        private int _currentThrust;
        private int _currentSpin;

        public ParticleSystem ThrusterOne;
        public ParticleSystem ThrusterTwo;

        public void DoThrust(int spin, int thrust)
        {
            if (_currentThrust != thrust || _currentSpin != spin)
            {
                _currentThrust = thrust;
                _currentSpin = spin;
            }

            // turn thrusters on and off
            if (thrust == 0)
            {
                ThrusterOne.Stop();
                ThrusterOne.Stop();
            }
            else
            {
                Quaternion rotation;
                if (thrust > 0)
                {
                    rotation = Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    rotation = Quaternion.Euler(0, 0, 0);
                }

                ThrusterOne.transform.localRotation = rotation;
                ThrusterOne.Play();
                ThrusterOne.transform.localRotation = rotation;
                ThrusterOne.Play();
            }

            thrusting = thrust;
            spinning = spin;
        }

    }
}
