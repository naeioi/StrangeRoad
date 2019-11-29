using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float initialSpeed = 8;
    public float accelerateRate = 0.1f;
    public float level;
    public int levelCounter;
    public float speed;
    public float speedR;
    public float speedOrient;
    public Material playerMat;
    public Color c1;
    public Color c2;
    public Color cSmash;

    public GameDefine.Direction direction;

    public bool isGameover = false;

    public RoadUnit currentRoad;
    public Rigidbody rigidBody;
    Vector3 targetOrient;
    public bool running;
    public bool isSmashing;
    public bool isSlowingDown;
    public double slowdownTime;

    public bool smashing
    {
        get
        {
            return isSmashing;
        }
    }
    public bool slowingDown
    {
        get
        {
            return isSlowingDown;
        }
    }

    Vector3 velocity
    {
        get
        {
            // return Vector3.zero;
            return running ? (speed) * MovementsByDirection[(int)direction] : Vector3.zero;
        }
    }

    Quaternion orientation
    {
        get
        {
            return OrientByDirection[(int)direction];
        }
    }

    Vector3 angularVelocity
    {
        get {
            return running? speedR * RotationsByDirection[(int)direction] : Vector3.zero;
        }
}

    readonly Vector3[] MovementsByDirection =
    {
        Vector3.left,
        Vector3.right,
        Vector3.forward
    };

    readonly Vector3[] RotationsByDirection =
    {
        Vector3.forward,
        Vector3.back,
        Vector3.right
    };

    readonly Quaternion[] OrientByDirection =
    {
        Quaternion.Euler(0, -90, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 0, 0)
    };

    void Awake()
    {
        isGameover = false;
        isSmashing = false;
        isSlowingDown = false;
        speed = initialSpeed;
        levelCounter = 0;
        level = 0.0f;
        MainCanvas.Instance.inGameScript.SetLevelTxt(1);
    }

    private void Start()
    {
    }

    public void Stop()
    {
        running = false;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public void SetDirection(GameDefine.Direction direction)
    {
        this.direction = direction;
        rigidBody.angularVelocity = angularVelocity;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
        // Move ball to the center of the track
        Vector3 pathCenter = transform.position;
        if (currentRoad.direction == GameDefine.Direction.Forward)
            pathCenter.x = currentRoad.startPoint.x;
        else
            pathCenter.z = currentRoad.startPoint.z;
        transform.position = Vector3.Lerp(transform.position, pathCenter, 2 * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, orientation, Time.deltaTime * speedOrient);
        rigidBody.angularVelocity = angularVelocity;
    }

    public void levelUp(){
        level += 1;
        MainCanvas.Instance.inGameScript.SetLevelTxt((int)level+1);
        if(!isSlowingDown)
            speed = initialSpeed + level * accelerateRate;
        levelCounter = 0;
    }

    public void Run()
    {
        running = true;
        SetDirection(direction);
    }

    public void SetSmash(bool isSmashing)
    {
        this.isSmashing = isSmashing;

        if (isSmashing)
        {
            playerMat.SetColor("_Color", cSmash);
        }
        else
        {
            // Assume always blue
            playerMat.SetColor("_Color", c1);
        }
    }
    public void SetSlowDown(bool isSlowingDown)
    {
        this.isSlowingDown = isSlowingDown;

        if (isSlowingDown)
        {
            speed = initialSpeed / 2;
        }
        else
        {
            speed = initialSpeed + level * accelerateRate;
        }
    }
}
