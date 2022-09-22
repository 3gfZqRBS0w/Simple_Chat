using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SimpleChat.interfaces;


using System.Xml;
using System.Xml.Serialization;

namespace SimpleChat.communications
{
    public static class Connection
    {

        //Default values

        private static Random rand = new Random();

        public static int port = 2401;
        public static int decryptionCode = 0;
        public static string username = "Lombres";
        public static IPAddress IpAddress = IPAddress.Parse("127.0.0.1");
        public static UdpClient udp = new UdpClient(Connection.port);






        public static string MessageToXml(Message message)
        {
            string resultat = "";
            var serializer = new XmlSerializer(typeof(Message));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, message);
                    resultat = sww.ToString();
                }
            }
            return resultat;
        }

        public static Message XMlToMessage(string message)
        {

            var serializer = new XmlSerializer(typeof(Message));
            Message result;

            using (TextReader reader = new StringReader(message))
            {
                result = (Message)serializer.Deserialize(reader);
            }

            return result;
        }

        public static void SendPacket(Message message)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Console.WriteLine(Connection.MessageToXml(message));
            byte[] sendbuf = Encoding.ASCII.GetBytes(Connection.MessageToXml(message));
            IPEndPoint ep = new IPEndPoint(IpAddress, Connection.port);


            s.SendTo(sendbuf, ep);

            Console.WriteLine("Message sent to the broadcast address");
        }

        public static void listenConnection(IAsyncResult ar)
        {
            Console.WriteLine("Waiting for a package");
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, Connection.port);
            byte[] bytes = udp.EndReceive(ar, ref ip);
            string messageXML = Encoding.ASCII.GetString(bytes);

            Message message = Connection.XMlToMessage(messageXML);
            Console.WriteLine(messageXML);

            Program.AddMessage(message);


            Connection.udp.BeginReceive(Connection.listenConnection, new object());
        }

    }

    // server.Stop();






}




