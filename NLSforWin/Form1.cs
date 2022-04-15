using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace NLSforWin
{
    public partial class Form1 : Form
    {

        public int location;
        public static int flag;

        //レジストリ値検索
        string rKeyName = @"SOFTWARE\Policies\Microsoft\Windows\Personalization";
        string rGetValueName = "NoLockScreen";

        public Form1()
        {

            InitializeComponent();

            try//レジストリ値がある
            {
                RegistryKey rKey = Registry.LocalMachine.OpenSubKey(rKeyName);
                location = (int)rKey.GetValue(rGetValueName);
                rKey.Close();
            }
            catch (NullReferenceException)//ない場合
            {
                location = 0;
            }

            if (location == 0)//0 有効 1 無効
            {
                label1.Text = "現在、ロック画面は有効になっています";
                button1.Text = "無効化する";
            }
            else
            {
                label1.Text = "現在、ロック画面は無効になっています";
                button1.Text = "有効化する";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (location == 0)//ロック画面が有効ならば、無効化する。有効化オプションを提示
            {
                location = 1;
                try
                {
                    // レジストリ・キーを新規作成して開く
                    RegistryKey rKey = Registry.LocalMachine.CreateSubKey(rKeyName);

                    // レジストリの値を設定
                    rKey.SetValue(rGetValueName, location);

                    // 開いたレジストリを閉じる
                    rKey.Close();

                }
                catch (Exception ex)
                {
                    // レジストリ・キーが存在しない
                    MessageBox.Show(ex.Message);
                }
                
                button1.Text = "有効化する";
                label1.Text = "現在、ロック画面は無効になっています";
            }
            else//無効ならば、有効化する。無効化オプションを提示
            {

                location = 0;
                try
                {
                    // レジストリ・キーを新規作成して開く
                    RegistryKey rKey = Registry.LocalMachine.CreateSubKey(rKeyName);

                    // レジストリの値を設定
                    rKey.SetValue(rGetValueName, location);

                    // 開いたレジストリを閉じる
                    rKey.Close();
                }
                catch (Exception ex)
                {
                    // レジストリ・キーが存在しない
                    MessageBox.Show(ex.Message);
                }

                button1.Text = "無効化する";
                label1.Text = "現在、ロック画面は有効になっています";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://7ka.org/software/nolockscreen8");
        }
    }
}
