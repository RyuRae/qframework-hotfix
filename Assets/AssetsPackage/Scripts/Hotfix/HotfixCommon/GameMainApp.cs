using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace MsbFramework
{
    /// <summary>
    /// �ܹ�������������ģ��ע�ἰ�ַ�
    /// ע��Model����ȡ����
    /// ע��System����ȡϵͳ
    /// ע��Utility����ȡ������
    /// </summary>
    public class GameMainApp : Architecture<GameMainApp>
    {
        protected override void Init()
        {
            //ע������ģ��
            //this.RegisterModel<IModel>();
            
            //ע��ϵͳ��
            //this.RegisterSystem<ISystem>();
            
            //ע�Ṥ����
            //this.RegisterUtility<IUtility>();

        }
    }
}
