using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 探索パート時のスクリプト
/// プレイヤーの移動関連
/// </summary>
public class Con_Player : MonoBehaviour
{
    #region //! プレイヤー移動
    [Header("速度")] 
        public float move_Speed;
    [Header("ジャンプ力")]
        public float move_Jump;
    [System.NonSerialized]
        public float vec_speed; // 現在の速度を測る

    private Rigidbody rb = null;
    #endregion

    #region //! 接地関連
    [Header("接地判定")] public check_ground c_ground;
    private bool isGround; // 接地しているかどうか
    #endregion

    #region //!カメラ関連
    [Header("追従させるカメラ")]
        public GameObject mainCamera = null;
    private Vector3 vec_player; // プレイヤーの座標
    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Debug.Log(Vec_speed);
        vec_speed = rb.velocity.magnitude;  // 現在の速度を計測
        isGround = c_ground.IsGround();     // 接地状態の更新
        vec_player = this.transform.position;// 現在の座標を更新

        // 移動の入力
        float speed_x = Move_X();
        float speed_y = Jump();
        float speed_z = Move_Z();
        // 一定以上の速度が出ていると加速度を0にする
        if (vec_speed >= 30)
        {
            speed_x = 0;
            speed_z = 0;
        }
        rb.AddForce(speed_x, speed_y, speed_z);
        Move_Camera();
    }

    /// <summary>
    /// カメラの制御
    /// </summary>
    void Move_Camera()
    {
        // プレイヤーの現在座標
        float x_p = vec_player.x;
        float y_p = vec_player.y;
        float z_p = vec_player.z;
        
        // カメラの座標更新
        mainCamera.transform.position = new Vector3(x_p + 0.0f,
                                                    y_p + 2.2f,
                                                    z_p - 5.0f);
        
    }

    /// <summary>
    /// x軸の移動入力
    /// </summary>
    /// <returns></returns>
    float Move_X()
    {
        float x = Input.GetAxis("Horizontal");
        x *= move_Speed;
        return x;
    }

    /// <summary>
    /// Y軸の移動入力(ジャンプの処理)
    /// </summary>
    /// <returns></returns>
    float Jump()
    {
        float y = 0;
        // 次の条件下でジャンプする
        // 「スペースキーを押されたとき」かつ「地面に接地しているとき」
        if(Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            y = move_Jump;
        }
        else
        {
            y = 0;
        }

        return y;
    }

    /// <summary>
    /// z軸の移動入力
    /// </summary>
    /// <returns></returns>
    float Move_Z()
    {
        float z = Input.GetAxis("Vertical");
        z *= move_Speed;
        return z;
    }
}
