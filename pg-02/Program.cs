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

            string message = "MySQL �ڑ�����";

            WriteLine( "TEST:" + message );
            WriteLine( string.Format("TEST:{0}", message) );
            WriteLine( $"TEST:{message}" );
            WriteLine( @"C:\app\cs21" );
            WriteLine( $@"C:\app\cs21\ConsoleAppMySQL02 : {message}" );

            // MySQL �̏���

            // SQL
            string myQuery = "SELECT �Ј��}�X�^.*,DATE_FORMAT(���N����,'%Y-%m-%d') as �a���� from �Ј��}�X�^";

            // SQL���s�p�̃I�u�W�F�N�g���쐬
            OdbcCommand myCommand = new OdbcCommand();

            // ���s�p�I�u�W�F�N�g�ɕK�v�ȏ���^����
            myCommand.CommandText = myQuery;    // SQL
            myCommand.Connection = myCon;       // �ڑ�

            // ���ł���A�f�[�^�x�[�X�̒l�����炤�ׂ̃I�u�W�F�N�g�̕ϐ��̒�`
            OdbcDataReader myReader;

            // SELECT �����s�������ʂ��擾
            myReader = myCommand.ExecuteReader();

            // myReader ����f�[�^���ǂ݂������Ԃ����ƃ��[�v
            while (myReader.Read())
            {
                string text = $"{myReader.GetValue(myReader.GetOrdinal("����")).ToString()}";
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
            // �ڑ�������̍쐬
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
            builder.Driver = "MySQL ODBC 8.0 Unicode Driver";
            // �ڑ��p�̃p�����[�^��ǉ�
            builder.Add("server", "localhost");
            builder.Add("database", "lightbox");
            builder.Add("uid", "root");
            builder.Add("pwd", "");

            string work = builder.ConnectionString;

            Console.WriteLine(builder.ConnectionString);

            // �ڑ��̍쐬
            OdbcConnection myCon = new OdbcConnection();

            // MySQL �̐ڑ���������
            myCon.ConnectionString = builder.ConnectionString;

            // MySQL �ɐڑ�
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