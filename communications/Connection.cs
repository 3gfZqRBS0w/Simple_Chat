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
        public static string decryptionCode = "pCw0bX$7OLQEI1!o^y%nc3^#";
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


            // encrypt the message
            
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Console.WriteLine(Connection.MessageToXml(message));
             try {
            byte[] sendbuf = Encoding.ASCII.GetBytes(Encryption.Encrypt(Connection.MessageToXml(message),decryptionCode));
        
            IPEndPoint ep = new IPEndPoint(IpAddress, Connection.port);


            s.SendTo(sendbuf, ep);
             }
             catch (Exception e) {
                Console.WriteLine(e.Message);
             }

            
        }

        public static void listenConnection(IAsyncResult ar)
        {
            Console.WriteLine("Waiting for a package");
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, Connection.port);
            byte[] bytes = udp.EndReceive(ar, ref ip);
            string messageXML = Encryption.Decrypt(Encoding.ASCII.GetString(bytes),decryptionCode);

            try {
                Message message = Connection.XMlToMessage(messageXML);
                Program.AddMessage(message);
                Console.WriteLine(messageXML);
            }
            catch (Exception e) {
                Console.WriteLine("Bad code") ; 
            }

            

            


            Connection.udp.BeginReceive(Connection.listenConnection, new object());
        }

    }

    // server.Stop();






}




