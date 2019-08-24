using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Util;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Web;
using System.Configuration;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Zuowj.Core
{
    /// <summary>
    /// 网上的RabbitMQ 连接缓冲池的例子代码
    /// </summary>
    public class MQHelper
    {
        private const string CacheKey_MQConnectionSetting = "MQConnectionSetting";
        private const string CacheKey_MQMaxConnectionCount = "MQMaxConnectionCount";

        private readonly static ConcurrentQueue<IConnection> FreeConnectionQueue;//空闲连接对象队列
        private readonly static ConcurrentDictionary<IConnection, bool> BusyConnectionDic;//使用中（忙）连接对象集合
        private readonly static ConcurrentDictionary<IConnection, int> MQConnectionPoolUsingDicNew;//连接池使用率
        private readonly static Semaphore MQConnectionPoolSemaphore;
        private readonly static object freeConnLock = new object(), addConnLock = new object();
        private static int connCount = 0;

        public const int DefaultMaxConnectionCount = 30;//默认最大保持可用连接数
        public const int DefaultMaxConnectionUsingCount = 10000;//默认最大连接可访问次数


        private static int MaxConnectionCount => 100;

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="hostName">服务器地址</param>
        /// <param name="userName">登录账号</param>
        /// <param name="passWord">登录密码</param>
        /// <returns></returns>
        private static ConnectionFactory CrateFactory()
        {
            var mqConnectionSetting = GetMQConnectionSetting();
            var connectionfactory = new ConnectionFactory();
            connectionfactory.HostName = mqConnectionSetting[0];
            connectionfactory.UserName = mqConnectionSetting[1];
            connectionfactory.Password = mqConnectionSetting[2];
            if (mqConnectionSetting.Length > 3) //增加端口号
            {
                connectionfactory.Port = Convert.ToInt32(mqConnectionSetting[3]);
            }
            return connectionfactory;
        }

        private static string[] GetMQConnectionSetting()
        {
            string[] mqConnectionSetting = null;
            return mqConnectionSetting;
        }




        public static IConnection CreateMQConnection()
        {
            var factory = CrateFactory();
            factory.AutomaticRecoveryEnabled = true;//自动重连
            var connection = factory.CreateConnection();
            return connection;
        }


        static MQHelper()
        {
            FreeConnectionQueue = new ConcurrentQueue<IConnection>();
            BusyConnectionDic = new ConcurrentDictionary<IConnection, bool>();
            MQConnectionPoolUsingDicNew = new ConcurrentDictionary<IConnection, int>();//连接池使用率
            MQConnectionPoolSemaphore = new Semaphore(MaxConnectionCount, MaxConnectionCount, "MQConnectionPoolSemaphore");//信号量，控制同时并发可用线程数

        }

        public static IConnection CreateMQConnectionInPoolNew()
        {

        SelectMQConnectionLine:

            MQConnectionPoolSemaphore.WaitOne();//当<MaxConnectionCount时，会直接进入，否则会等待直到空闲连接出现

            IConnection mqConnection = null;
            if (FreeConnectionQueue.Count + BusyConnectionDic.Count < MaxConnectionCount)//如果已有连接数小于最大可用连接数，则直接创建新连接
            {
                lock (addConnLock)
                {
                    if (FreeConnectionQueue.Count + BusyConnectionDic.Count < MaxConnectionCount)
                    {
                        mqConnection = CreateMQConnection();
                        BusyConnectionDic[mqConnection] = true;//加入到忙连接集合中
                        MQConnectionPoolUsingDicNew[mqConnection] = 1;
                        //  BaseUtil.Logger.DebugFormat("Create a MQConnection:{0},FreeConnectionCount:{1}, BusyConnectionCount:{2}", mqConnection.GetHashCode().ToString(), FreeConnectionQueue.Count, BusyConnectionDic.Count);
                        return mqConnection;
                    }
                }
            }


            if (!FreeConnectionQueue.TryDequeue(out mqConnection)) //如果没有可用空闲连接，则重新进入等待排队
            {
                // BaseUtil.Logger.DebugFormat("no FreeConnection,FreeConnectionCount:{0}, BusyConnectionCount:{1}", FreeConnectionQueue.Count, BusyConnectionDic.Count);
                goto SelectMQConnectionLine;
            }
            else if (MQConnectionPoolUsingDicNew[mqConnection] + 1 > DefaultMaxConnectionUsingCount || !mqConnection.IsOpen) //如果取到空闲连接，判断是否使用次数是否超过最大限制,超过则释放连接并重新创建
            {
                mqConnection.Close();
                mqConnection.Dispose();
                // BaseUtil.Logger.DebugFormat("close > DefaultMaxConnectionUsingCount mqConnection,FreeConnectionCount:{0}, BusyConnectionCount:{1}", FreeConnectionQueue.Count, BusyConnectionDic.Count);

                mqConnection = CreateMQConnection();
                MQConnectionPoolUsingDicNew[mqConnection] = 0;
                // BaseUtil.Logger.DebugFormat("create new mqConnection,FreeConnectionCount:{0}, BusyConnectionCount:{1}", FreeConnectionQueue.Count, BusyConnectionDic.Count);
            }

            BusyConnectionDic[mqConnection] = true;//加入到忙连接集合中
            MQConnectionPoolUsingDicNew[mqConnection] = MQConnectionPoolUsingDicNew[mqConnection] + 1;//使用次数加1

            // BaseUtil.Logger.DebugFormat("set BusyConnectionDic:{0},FreeConnectionCount:{1}, BusyConnectionCount:{2}", mqConnection.GetHashCode().ToString(), FreeConnectionQueue.Count, BusyConnectionDic.Count);

            return mqConnection;
        }

        private static void ResetMQConnectionToFree(IConnection connection)
        {
            lock (freeConnLock)
            {
                bool result = false;
                if (BusyConnectionDic.TryRemove(connection, out result)) //从忙队列中取出
                {
                    //  BaseUtil.Logger.DebugFormat("set FreeConnectionQueue:{0},FreeConnectionCount:{1}, BusyConnectionCount:{2}", connection.GetHashCode().ToString(), FreeConnectionQueue.Count, BusyConnectionDic.Count);
                }
                else
                {
                    // BaseUtil.Logger.DebugFormat("failed TryRemove BusyConnectionDic:{0},FreeConnectionCount:{1}, BusyConnectionCount:{2}", connection.GetHashCode().ToString(), FreeConnectionQueue.Count, BusyConnectionDic.Count);
                }

                if (FreeConnectionQueue.Count + BusyConnectionDic.Count > MaxConnectionCount)//如果因为高并发出现极少概率的>MaxConnectionCount，则直接释放该连接
                {
                    connection.Close();
                    connection.Dispose();
                }
                else
                {
                    FreeConnectionQueue.Enqueue(connection);//加入到空闲队列，以便持续提供连接服务
                }

                MQConnectionPoolSemaphore.Release();//释放一个空闲连接信号

                //Interlocked.Decrement(ref connCount);
                //BaseUtil.Logger.DebugFormat("Enqueue FreeConnectionQueue:{0},FreeConnectionCount:{1}, BusyConnectionCount:{2},thread count:{3}", connection.GetHashCode().ToString(), FreeConnectionQueue.Count, BusyConnectionDic.Count,connCount);
            }
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="connection">消息队列连接对象</param>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static string SendMsg(IConnection connection, string queueName, string msg, bool durable = true)
        {
            try
            {

                using (var channel = connection.CreateModel())//建立通讯信道
                {
                    // 参数从前面开始分别意思为：队列名称，是否持久化，独占的队列，不使用时是否自动删除，其他参数
                    channel.QueueDeclare(queueName, durable, false, false, null);

                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;//1表示不持久,2.表示持久化

                    if (!durable)
                        properties = null;

                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish("", queueName, properties, body);
                }


                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                ResetMQConnectionToFree(connection);
            }
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="connection">消息队列连接对象</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="dealMessage">消息处理函数</param>
        /// <param name="saveLog">保存日志方法，可选</param>
        public static void ConsumeMsg(IConnection connection, string queueName, bool durable, Func<string, ConsumeAction> dealMessage, Action<string, Exception> saveLog = null)
        {
            try
            {

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, durable, false, false, null); //获取队列 
                    channel.BasicQos(0, 1, false); //分发机制为触发式

                    var consumer = new QueueingBasicConsumer(channel); //建立消费者
                    // 从左到右参数意思分别是：队列名称、是否读取消息后直接删除消息，消费者
                    channel.BasicConsume(queueName, false, consumer);

                    while (true)  //如果队列中有消息
                    {
                        ConsumeAction consumeResult = ConsumeAction.RETRY;
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue(); //获取消息
                        string message = null;

                        try
                        {
                            var body = ea.Body;
                            message = Encoding.UTF8.GetString(body);
                            consumeResult = dealMessage(message);
                        }
                        catch (Exception ex)
                        {
                            if (saveLog != null)
                            {
                                saveLog(message, ex);
                            }
                        }
                        if (consumeResult == ConsumeAction.ACCEPT)
                        {
                            channel.BasicAck(ea.DeliveryTag, false);  //消息从队列中删除
                        }
                        else if (consumeResult == ConsumeAction.RETRY)
                        {
                            channel.BasicNack(ea.DeliveryTag, false, true); //消息重回队列
                        }
                        else
                        {
                            channel.BasicNack(ea.DeliveryTag, false, false); //消息直接丢弃
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (saveLog != null)
                {
                    saveLog("QueueName:" + queueName, ex);
                }

                throw ex;
            }
            finally
            {
                ResetMQConnectionToFree(connection);
            }
        }


        /// <summary>
        /// 依次获取单个消息
        /// </summary>
        /// <param name="connection">消息队列连接对象</param>
        /// <param name="QueueName">队列名称</param>
        /// <param name="durable">持久化</param>
        /// <param name="dealMessage">处理消息委托</param>
        public static void ConsumeMsgSingle(IConnection connection, string QueueName, bool durable, Func<string, ConsumeAction> dealMessage)
        {
            try
            {

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, durable, false, false, null); //获取队列 
                    channel.BasicQos(0, 1, false); //分发机制为触发式

                    uint msgCount = channel.MessageCount(QueueName);

                    if (msgCount > 0)
                    {
                        var consumer = new QueueingBasicConsumer(channel); //建立消费者
                        // 从左到右参数意思分别是：队列名称、是否读取消息后直接删除消息，消费者
                        channel.BasicConsume(QueueName, false, consumer);

                        ConsumeAction consumeResult = ConsumeAction.RETRY;
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue(); //获取消息
                        try
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            consumeResult = dealMessage(message);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (consumeResult == ConsumeAction.ACCEPT)
                            {
                                channel.BasicAck(ea.DeliveryTag, false);  //消息从队列中删除
                            }
                            else if (consumeResult == ConsumeAction.RETRY)
                            {
                                channel.BasicNack(ea.DeliveryTag, false, true); //消息重回队列
                            }
                            else
                            {
                                channel.BasicNack(ea.DeliveryTag, false, false); //消息直接丢弃
                            }
                        }
                    }
                    else
                    {
                        dealMessage(string.Empty);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ResetMQConnectionToFree(connection);
            }
        }


        /// <summary>
        /// 获取队列消息数
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="QueueName"></param>
        /// <returns></returns>
        public static int GetMessageCount(IConnection connection, string QueueName)
        {
            int msgCount = 0;
            try
            {

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null); //获取队列 
                    msgCount = (int)channel.MessageCount(QueueName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ResetMQConnectionToFree(connection);
            }

            return msgCount;
        }


    }

    public enum ConsumeAction
    {
        ACCEPT,  // 消费成功
        RETRY,   // 消费失败，可以放回队列重新消费
        REJECT,  // 消费失败，直接丢弃
    }
}
