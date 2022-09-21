using System ;
using System.Net;
using System.Net.Sockets;

using System.Xml;
using System.Xml.Serialization;


namespace SimpleChat.communications {

    [XmlRoot("message")]
    public class Message {

        [XmlElement("content")]
        public string content ;

        [XmlElement("date")]
        public DateTime date ;
        [XmlElement("senderName")]
        public string senderName ; 

        [XmlIgnore]
        public IPAddress sender ;

        public Message() {}

        public Message(string content, DateTime date, string senderName, IPAddress sender) {
            this.content = content;
            this.date = date;
            this.senderName = senderName;
            this.sender = sender;
        }

    public string Content {
      get {return this.content; }
    }

    public DateTime Date {
      get {return this.date; }
     }

    public string SenderName {
      get {return this.senderName; }
     }

        public string getContent() {
            return this.content;
        }

        public string getSenderName() {
            return this.senderName;
        }

        public string getDate() {
            return date.ToString("u");
        }




 
        
    }
}