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
    [System.NonSerialized]
    public float vec_speed; // 現在の速度を測る

    private Rigidbody rb = null;
    #endregion

    /*
    #region //! 接地関連
    [Header("接地判定")] public check_ground c_ground;
    private bool isGround; // 接地しているかどうか
    #endregion
    */

    #region //!カメラ関連
    [Header("追従させるカメラ")]
    public GameObject mainCamera = null;
    private Vector3 vec_player; // プレイヤーの座標
    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // プレイヤーの座標情報が保存されていたら反映する
        if (Con_Player2.player.exist)
        {
            this.transform.position = new Vector3(Con_Player2.player.pos_x,
                                                  Con_Player2.player.pos_y,
                                                  Con_Player2.player.pos_z);
            Con_Player2.player.exist = false;
        }
    }

    private void Update()
    {
        vec_speed = rb.velocity.magnitude;  // 現在の速度を計測
        // isGround = c_ground.IsGround();     // 接地状態の更新
        vec_player = this.transform.position;// 現在の座標を更新

        Move_main();
        Move_Camera();

        // プレイヤーの座標情報の保存をする
        if (Con_Player2.player.exist)
        {
            Con_Player2.player.pos_x = vec_player.x;
            Con_Player2.player.pos_y = vec_player.y;
            Con_Player2.player.pos_z = vec_player.z;
        }
    }

    /// <summary>
    /// カメラの制御
    /// </summary>
    void Move_Camera()
    {
        // カメラの座標更新
        mainCamera.transform.position = new Vector3(vec_player.x + 0.0f,
                                                    vec_player.y + 2.2f,
                                                    vec_player.z - 5.0f);

    }


    void Move_main()
    {
        float _speed_x = move_Speed * Input.GetAxis("Horizontal");
        float _speed_z = move_Speed * Input.GetAxis("Vertical");
        rb.velocity = new Vector3(_speed_x, 0, _speed_z);
    }
}