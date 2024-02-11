using UnityEngine;
using UnityEngine.UI;

// 情報表示用の UI を制御するコンポーネント
public class hud : MonoBehaviour
{
    public Image m_hpGauge; // HP ゲージ
    [Header("hp減アニメーション")]
    public AnimationCurve hpCurve;
    private float moveTime; 

    private float prevhp;
    float d_hp = 0;

    private void Start() {
        var player = Player.m_instance;

        // HP のゲージの表示を更新する
        var hp = player.m_hp;
        var hpMax = player.m_hpMax;
        prevhp = hpMax;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // プレイヤーを取得する
        var player = Player.m_instance;
        var hpMax = player.m_hpMax;
        // HP のゲージの表示を更新する
        var hp = player.m_hp;
        if(hp != prevhp){
            moveTime = 0;
            d_hp = (float)((prevhp - hp)/hpMax);
        }
        else{
        }
        var gauge = hpCurve.Evaluate(moveTime) * d_hp;
        moveTime += 0.08f;
        m_hpGauge.fillAmount = (float)(prevhp/hpMax) - gauge;
        if(moveTime >= 1){
            moveTime = 0;
            d_hp = 0;
            }

        prevhp = hp;

    }
}
