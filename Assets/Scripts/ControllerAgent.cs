using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Cysharp.Threading.Tasks;
using uOSC;

public class ControllerAgent : Agent
{

    public GameObject gunObject;
    public GameObject playerObject;
    public GameObject managerObject;

    uOscClient client;

    //private static readonly string[] Emotions = new string[] { "Good", "Neutral", "Bad" };
    // Start is called before the first frame update
    public override void Initialize()
    {
        client = GetComponent<uOSC.uOscClient>();
        
    }

    public override void OnEpisodeBegin()
        {
            var player = Player_c.m_instance;
            var hp = player.m_hp;
            var hpMax = player.m_hpMax;
            var _manager = Manager.m_instance;

            Debug.Log("OnEpisodeBegin()");
            // 機嫌切り替え
            //_manager.emote = Emotions.ElementAt(UnityEngine.Random.Range(0, Emotions.Count()));
            // HPが満タンじゃなかったら満タンにする
            if (hp != hpMax)
            {
                playerObject.GetComponent<Player_c>().m_hp = hpMax;
            }
        }

        /// 観察値の設定
        public override void CollectObservations(VectorSensor sensor)
        {
            var _manager = managerObject.GetComponent<Manager>();
            var player = Player_c.m_instance;
            var hp = player.m_hp;
            var enemyCount = 1;
            
            sensor.AddObservation(_manager.feeling);
            sensor.AddObservation(hp);

            //sensor.AddObservation(player.transform.position.x);
            //sensor.AddObservation(player.transform.position.y);

            sensor.AddObservation(gunObject.transform.position.x);
            sensor.AddObservation(gunObject.transform.position.y);
            sensor.AddObservation(gunObject.transform.eulerAngles);
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            sensor.AddObservation(enemy.transform.position.x);
            sensor.AddObservation(enemy.transform.position.y);
            sensor.AddObservation(enemy.GetComponent<Enemy_c>().m_hp);

            if (enemyCount++ >= 20)
            {
                break;
            }
        }
        for (int i = enemyCount;i <= 20;i++)
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            var _manager = managerObject.GetComponent<Manager>();
            var actionType = actions.DiscreteActions[0];

            switch(_manager.feeling){
                case 3:
                if (actionType == 1){
                client.Send("/motorA", "fwd", 15);
                }

                if (actionType == 2){
                    client.Send("/motorA", "fwd", 30);
                    }

                if (actionType == 3){
                    client.Send("/motorA", "rev", 15);
                    }

                if (actionType == 4){
                    client.Send("/motorA", "rev", 30);
                    }

                if (actionType == 5){
                    client.Send("/motorA", "hld", 0);
                    AddReward(-0.5f);
                    }

                if (actionType == 6){
                    client.Send("/motorA", "stp", 0);
                    AddReward(0.01f);
                    }

                if (actionType == 7){
                    }
                break;
                
                case 2:
                if (actionType == 1){
                client.Send("/motorA", "fwd", 20);
                }

                if (actionType == 2){
                    client.Send("/motorA", "rev", 20);
                    }

                if (actionType == 3){
                    client.Send("/motorA", "stp", 0);
                    }

                if (actionType == 4){
                    client.Send("/motorA", "stp", 0);
                    }

                if (actionType == 5){
                    client.Send("/motorA", "stp", 0);
                    }

                if (actionType == 6){
                    client.Send("/motorA", "stp", 0);
                    AddReward(0.02f);
                    }
                    
                if (actionType == 7){
                    }
                break;

                case 1:
                if (actionType == 1){
                client.Send("/motorA", "fwd", 15);
                }

                if (actionType == 2){
                    client.Send("/motorA", "fwd", 30);
                    }

                if (actionType == 3){
                    client.Send("/motorA", "rev", 15);
                    }

                if (actionType == 4){
                    client.Send("/motorA", "rev", 30);
                    }

                if (actionType == 5){
                    client.Send("/motorA", "hld", 0);
                    }

                if (actionType == 6){
                    client.Send("/motorA", "stp", 0);
                    AddReward(-0.02f);
                    }

                if (actionType == 7){
                    }
                break;

            }
            
            
            AddReward(0.01f);
            
            }
            public void c_AddReward(float reward,float reward2,float reward3)
            {
                var _manager = managerObject.GetComponent<Manager>();
                if (_manager.feeling == 1)
                {
                    AddReward(reward);
                }
                else if(_manager.feeling == 2){
                    AddReward(reward2);
                }
                else if(_manager.feeling == 3){AddReward(reward3);}
            }
            public void broken()
            {
                var _manager = managerObject.GetComponent<Manager>();
                if (_manager.feeling == 1)
                {
                    AddReward(1.0f);
                    EndEpisode();
                }
                else if(_manager.feeling == 3){
                    AddReward(-1.0f);
                    EndEpisode();
                }
                else if(_manager.feeling == 2){
                    EndEpisode();
                }
            }
    private void OnDestroy() {
   }
}
