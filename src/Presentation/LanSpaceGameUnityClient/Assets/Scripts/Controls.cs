using UnityEngine;
using UnityEngine.Networking;

public class Controls : NetworkBehaviour
{
    [SyncVar]
    float thrusting;
    float spin;

    int oldThrust;
    int oldSpin;


    float rotateSpeed = 200f;
    float acceleration = 8f;
    float bulletSpeed = 12f;

    public GameObject bulletPrefab;

    public ParticleSystem thruster1;
    public ParticleSystem thruster2;

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        GetMovement();

        // update thrust
        if (thrusting != 0)
        {
            Vector3 thrustVec = transform.right * thrusting * acceleration;
            GetComponent<Rigidbody2D>().AddForce(thrustVec);
        }

        // update rotation 
        float rotate = spin * rotateSpeed;
        GetComponent<Rigidbody2D>().angularVelocity = rotate;
    }

    private void GetMovement()
    {
        int newSpin = 0;
        if (Input.GetKey(KeyCode.A))
        {
            newSpin += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newSpin -= 1;
        }

        int newThrust = 0;
        if (Input.GetKey(KeyCode.W))
        {
            newThrust += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newThrust -= 1;
        }

        if (oldThrust != newThrust || oldSpin != newSpin)
        {
            DoThrust(newThrust, newSpin);
            oldThrust = newThrust;
            oldSpin = newSpin;
        }
    }

    void DoThrust(int newThrust, int newSpin)
    {
        // turn thrusters on and off
        if (newThrust == 0)
        {
            thruster1.Stop();
            thruster2.Stop();
        }
        else
        {
            Quaternion rot;
            if (newThrust > 0)
            {
                rot = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                rot = Quaternion.Euler(0, 0, 0);
            }
            thruster1.transform.localRotation = rot;
            thruster1.Play();
            thruster2.transform.localRotation = rot;
            thruster2.Play();
        }

        // apply new values
        this.thrusting = newThrust;
        this.spin = newSpin;
    }

    /*
	public override void OnDeSerializeVars(NetworkReader reader, int channelId, bool initialState)
	{
		if (initialState)
		{
			this.thrusting = reader.ReadSingle();
			return;
		}

		int num = (int)reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			this.thrusting = reader.ReadSingle();
		}
	}*/

}
