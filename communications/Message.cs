using System ;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SimpleChat.communications {
    public class Message {
        private readonly string content ;
        private readonly DateTime date ;
        private readonly string senderName ; 
        private readonly IPAddress sender ;

        public Message(string content, DateTime date, string senderName, IPAddress sender) {
            this.content = content;
            this.date = date;
            this.senderName = senderName;
            this.sender = sender;
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