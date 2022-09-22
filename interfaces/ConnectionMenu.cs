using System;
using Gtk;
using System.Text ; 
using System.Collections;
using SimpleChat.communications;
using SimpleChat.interfaces;
using System.Collections.Generic;
using Cairo;
using System.Net;
using System.Net.Sockets;

namespace SimpleChat.interfaces
{
    public class ConnectionMenu : Window
    {

        private VBox vbox;
        private HBox hbox, hbox0, hbox1, hbox2, hbox3;
        public ConnectionMenu() : base("Connection configuration")
        {
            SetDefaultSize(400, 200);
            SetPosition(WindowPosition.Center);
            

            BorderWidth = 20;

            hbox = new HBox(true, 1);
            hbox0 = new HBox(true, 1);
            hbox1 = new HBox(true, 1);
            hbox2 = new HBox(true, 1);
            hbox3 = new HBox(true, 1);
            vbox = new VBox(true, 1);

            Label label0 = new Label("Sending IP address");
            Entry entry0 = new Entry();


            Label label = new Label("Name");
            Entry entry = new Entry();


            Label label2 = new Label("Decryption Key");
            Entry decryptionKeyEntry = new Entry();

            Label label3 = new Label("Port");
            Entry portEntry = new Entry();

            entry.Text = Connection.username;
            decryptionKeyEntry.Text = Connection.decryptionCode.ToString();
            entry0.Text = Connection.IpAddress.ToString();
            portEntry.Text = Connection.port.ToString();



            Button confirmButton = new Button("Confirm");
            Button cancelButton = new Button("Cancel");

            // Address IP
            hbox.Add(label0) ;
            hbox.Add(entry0) ;

            // Name
            hbox0.Add(label);
            hbox0.Add(entry);

            // Descryption Key
            hbox1.Add(label2);
            hbox1.Add(decryptionKeyEntry);

            // port
            hbox2.Add(label3);
            hbox2.Add(portEntry);

            hbox3.Add(confirmButton);
            hbox3.Add(cancelButton);


            vbox.PackStart(hbox, false, false, 5);
            vbox.PackStart(hbox0, false, false, 5);
            vbox.PackStart(hbox1, false, false, 5);
            vbox.PackStart(hbox2, false, false, 5);
            vbox.PackEnd(hbox3, false, false, 5);

            

            confirmButton.Clicked += delegate
            {

                Connection.username = entry.Text;
                try
                {

                    int port = Int32.Parse(portEntry.Text) ;

                    string descryptionCode = decryptionKeyEntry.Text ;

                    try {
                        Connection.IpAddress = IPAddress.Parse(entry0.Text);
                        
                    }
                    catch( Exception e ) {
                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "IP address is not valid");
                        md.Run();
                        md.Destroy();
                        Hide();

                        return ; 
                    }

                    if (port < 0 || port > 65535)
                    {
                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "the port must be between 0 and 65535");
                        md.Run();
                        md.Destroy();
                        Hide();
                        return;
                    }

                    if (descryptionCode.Length != 24)
                    {
                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "The decryption key must be 24");
                        md.Run();
                        md.Destroy();
                        Hide();
                        return;
                    }

                    try {
                        // test
                        Encryption.Encrypt("blabla", descryptionCode) ;                 
                    }
                    catch (Exception ex) {
                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "input error :" + ex.Message);
                    md.Run();
                    md.Destroy();
                    Hide();
                    return;
                    }

                    Connection.port = port;
                    Connection.decryptionCode = descryptionCode;

                }
                catch (Exception ex)
                {
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "input error :" + ex.Message);
                    md.Run();
                    md.Destroy();
                    Hide();
                    return;
                }

                MessageDialog mdo = new MessageDialog(this,
                DialogFlags.DestroyWithParent, MessageType.Info,
                ButtonsType.Close, "input save");
                mdo.Run();
                mdo.Destroy();
                Hide();
                return;

            };

            cancelButton.Clicked += delegate
            {
                Hide();
            };

            Add(vbox);

            ShowAll();
        }


    }
}