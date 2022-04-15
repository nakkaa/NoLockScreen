using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NLSforWin
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// 
        
        //public static int flag;
        [STAThread]
        static void Main()
        {
            //多重起動の抑止
            string mutexName = "NLS11";
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName);

            bool hasHandle = false;
            try
            {
                try
                {
                    //ミューテックスの所有権を要求する
                    hasHandle = mutex.WaitOne(0, false);
                }
                //.NET Framework 2.0以降の場合
                catch (System.Threading.AbandonedMutexException)
                {
                    //別のアプリケーションがミューテックスを解放しないで終了した時
                    hasHandle = true;
                }
                //ミューテックスを得られたか調べる
                if (hasHandle == false)
                {
                    //得られなかった場合は、すでに起動していると判断して終了
                    return;
                }

                System.OperatingSystem os = System.Environment.OSVersion;
                //OSバージョンがWindows8.1でなければ終了する
                Version version = os.Version;
                if (version.Build < 9600)
                {
                    MessageBox.Show("このアプリケーションは\nWIndows 8.1 以降のOSに対応しています。", "エラー");
                    Environment.Exit(0);
                }

                //ウィンドウ生成
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            finally
            {
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }
                mutex.Close();
            }
            
        }
    }
}
