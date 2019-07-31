using System;
using System.Net;
using System.Net.Mail;

namespace SocketLib
{
    /// <summary>
    /// 发送email的实用工具类
    /// </summary>
    public class EMailUtils : MailMessage
    {
        #region 变量和属性

        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int SmtpServerPort { get; set; }

        /// <summary>
        /// 邮件服务器用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮件服务器密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SmtpClient对象
        /// </summary>
        private SmtpClient SmtpClient
        {
            get { return CreateSmtpClient( UserName, Password ); }
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 创建SmtpClient对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>SmtpClient对象</returns>
        private SmtpClient CreateSmtpClient( string userName, string password )
        {
            var client = new SmtpClient();

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
            client.Host = SmtpServer;

            //设置用于 SMTP 事务的端口，默认的是 25
            client.Port = SmtpServerPort;

            //这里才是真正的邮箱登陆名和密码，比如我的邮箱地址是 hbgx@hotmail， 我的用户名为 hbgx ，我的密码是 xgbh
            // client.Credentials = new System.Net.NetworkCredential("hbgx", "xgbh");
            if( !string.IsNullOrEmpty( userName ) && !string.IsNullOrEmpty( password ) )
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential( userName, password );
            }

            return client;
        }

        #endregion

        public EMailUtils()
        {
            // 默认的是 25
            SmtpServerPort = 25;

            BodyEncoding = System.Text.Encoding.UTF8;
            IsBodyHtml = true;

            Priority = MailPriority.Normal;
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        public void Send()
        {
            SmtpClient.Send( this );
        }

        /// <summary>
        /// 发送html邮件
        /// </summary>
        /// <param name="from">发送者，用于显示</param>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件体</param>
        /// <param name="to">接收者</param>
        /// <param name="cc">抄送者</param>
        public void Send( MailAddress from, string subject, string body, MailAddress[] to, MailAddress[] cc )
        {
            var mail = new EMailUtils();
            mail.From = from;
            mail.Subject = subject;
            mail.Body = body;
            
            Array.ForEach( to, address => mail.To.Add( address ) );

            if( cc != null )
            {
                Array.ForEach( cc, address => mail.To.Add( address ) );
            }

            Send( mail );
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public void Send( MailMessage mail )
        {
            SmtpClient.Send( mail );
        }
    }
}
