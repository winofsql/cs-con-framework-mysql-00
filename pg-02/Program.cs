using System;
using System.Data.Odbc;
using System.Diagnostics;

namespace cs_console_mysql_02
{
    class Program
    {
        static void Main(string[] args)
        {

            OdbcConnection myCon = CreateConnection();
            if ( myCon == null )
            {
                Environment.Exit(0);
            }

            string message = "MySQL 接続処理";

            WriteLine( "TEST:" + message );
            WriteLine( string.Format("TEST:{0}", message) );
            WriteLine( $"TEST:{message}" );
            WriteLine( @"C:\app\cs21" );
            WriteLine( $@"C:\app\cs21\ConsoleAppMySQL02 : {message}" );

            // MySQL の処理

            // SQL
            string myQuery = "SELECT 社員マスタ.*,DATE_FORMAT(生年月日,'%Y-%m-%d') as 誕生日 from 社員マスタ";

            // SQL実行用のオブジェクトを作成
            OdbcCommand myCommand = new OdbcCommand();

            // 実行用オブジェクトに必要な情報を与える
            myCommand.CommandText = myQuery;    // SQL
            myCommand.Connection = myCon;       // 接続

            // 次でする、データベースの値をもらう為のオブジェクトの変数の定義
            OdbcDataReader myReader;

            // SELECT を実行した結果を取得
            myReader = myCommand.ExecuteReader();

            // myReader からデータが読みだされる間ずっとループ
            while (myReader.Read())
            {
                string text = $"{myReader.GetValue(myReader.GetOrdinal("氏名")).ToString()}";
                WriteLine($"Debug:{text}");
            }

            myReader.Close();


            myCon.Close();

        }

        private static void WriteLine(string v)
        {
            Console.WriteLine(v);
            Debug.WriteLine(v);
        }

        static OdbcConnection CreateConnection()
        {
            // 接続文字列の作成
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
            builder.Driver = "MySQL ODBC 8.0 Unicode Driver";
            // 接続用のパラメータを追加
            builder.Add("server", "localhost");
            builder.Add("database", "lightbox");
            builder.Add("uid", "root");
            builder.Add("pwd", "");

            string work = builder.ConnectionString;

            Console.WriteLine(builder.ConnectionString);

            // 接続の作成
            OdbcConnection myCon = new OdbcConnection();

            // MySQL の接続準備完了
            myCon.ConnectionString = builder.ConnectionString;

            // MySQL に接続
            try
            {
                myCon.Open();

            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                myCon = null;

            }

            return myCon;
        }
    }
}