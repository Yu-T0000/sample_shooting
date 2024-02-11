using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵の出現位置の種類

// 敵を制御するコンポーネント
public class Enemy : MonoBehaviour
{
    [Header("行動タイプ")]
    public bool m_isFollow;

    public bool m_hasbullet;
    public enmbullet enmbulletPrefub;
    public Vector2 m_respawnPosInside; // 敵の出現位置（内側）
    public Vector2 m_respawnPosOutside; // 敵の出現位置（外側）
    public float m_speed; // 移動する速さ
    public float rotatespeed;
    public int m_hpMax; // HP の最大値
    public int m_exp; // この敵を倒した時に獲得できる経験値
    public int m_damage; // この敵がプレイヤーに与えるダメージ
    public float m_size; 

    private int m_hp; // HP
    private Vector3 m_direction; // 進行方向
    private float moveTime; 
    private float size; 

    private float D_size; 

    [Header("サイズカーブ")]
    public AnimationCurve sizeCurve;
    [Header("消滅")]
    public AnimationCurve destroyCurve; 
    // 敵が生成された時に呼び出される関数
    private void Start()
    {
        // HP を初期化する
        m_hp = m_hpMax;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        SizeController();
        transform.localScale = Vector2.one * size;
        transform.Rotate(0, 0, size * rotatespeed);
        if ( m_isFollow )
        {
        // プレイヤーの現在位置へ向かうベクトルを作成する
            var angle = util.GetAngle( 
                transform.localPosition, 
                Player.m_instance.transform.localPosition );
            var direction = util.GetDirection( angle );

            // プレイヤーが存在する方向に移動する
            transform.localPosition += direction * m_speed;
            return;
        }
        // まっすぐ移動する
        transform.localPosition += m_direction * m_speed;

    }


    // 敵が出現する時に初期化する関数
    public void Init( RESPAWN_TYPE respawnType )
    {
        var pos = Vector3.zero;

        // 指定された出現位置の種類に応じて、
        // 出現位置と進行方向を決定する
        switch ( respawnType )
        {
            // 出現位置が上の場合
            case RESPAWN_TYPE.UP:
                pos.x = Random.Range( 
                    -m_respawnPosInside.x, m_respawnPosInside.x );
                pos.y = m_respawnPosOutside.y;
                m_direction = Vector2.down;
                break;

            // 出現位置が右の場合
            case RESPAWN_TYPE.RIGHT:
                pos.x = m_respawnPosOutside.x;
                pos.y = Random.Range( 
                    0, m_respawnPosInside.y );
                m_direction = Vector2.left;
                break;

                 // 出現位置が下の場合
            case RESPAWN_TYPE.DOWN:
                pos.x = Random.Range( 
                    -m_respawnPosInside.x, m_respawnPosInside.x );
                pos.y = -m_respawnPosOutside.y;
                m_direction = Vector2.up;
                break;

            // 出現位置が左の場合
            case RESPAWN_TYPE.LEFT:
                pos.x = -m_respawnPosOutside.x;
                pos.y = Random.Range( 
                    0, m_respawnPosInside.y );
                m_direction = Vector2.right;
                break;
        }

        // 位置を反映する
        transform.localPosition = pos;
    }

     private void SizeController()
    {
        moveTime += Time.deltaTime;
        size = sizeCurve.Evaluate(moveTime) * m_size;
        if(moveTime >= 1){

            moveTime = 0;

        }
    }

    private void D_Controller()
    {   
        moveTime += Time.deltaTime;
        D_size = destroyCurve.Evaluate(moveTime) * m_size;
        if(moveTime >= 1){

            moveTime = 0;

        }
    }

    private void ShootNWay(float angleBace, float angleRange, float speed, int count){

        var pos = transform.localPosition;
        var rot = transform.localRotation;

        if(1 < count){
            for(int i = 0; i < count; ++i){
                var angle = angleBace + angleRange * ((float)i/(count - 1) - 0.5f);
                var shot = Instantiate(enmbulletPrefub, pos, rot);
                shot.Init(angle,speed);
            }
        }
        else if(count == 1){
            var shot = Instantiate(enmbulletPrefub, pos, rot);
                shot.Init(angleBace,speed);

        }
    }

    // 他のオブジェクトと衝突した時に呼び出される関数
private void OnTriggerEnter2D( Collider2D collision )
{
    if ( collision.name.Contains( "player" ) )
    {
    // プレイヤーにダメージを与える
    var player = collision.GetComponent<Player>();
    player.Damage( m_damage );
    Destroy( gameObject );
    return;
    }
    // 弾と衝突した場合
    if ( collision.name.Contains( "bullet_nom" ) )
    {
        // 弾を削除する
        Destroy( collision.gameObject );
        moveTime = 0;

        // 敵の HP を減らす
        m_hp--;

        // 敵の HP がまだ残っている場合はここで処理を終える
        if ( 0 < m_hp ) return;

        if(m_hasbullet){
            ShootNWay( 0, 360, 0.1f, 8);
        }
        // 敵を削除する
        Destroy( gameObject );
    }
}
}
