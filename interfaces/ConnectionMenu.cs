using System;
using Gtk;
using System.Collections;
using SimpleChat.communications;
using SimpleChat.interfaces;
using System.Collections.Generic;
using Cairo;

namespace SimpleChat.interfaces
{
    public class ConnectionMenu : Window
    {

        private VBox vbox;
        private HBox hbox0, hbox1, hbox2, hbox3;
        public ConnectionMenu() : base("Connection configuration")
        {
            SetDefaultSize(400, 200);
            SetPosition(WindowPosition.Center);
            BorderWidth = 20;


            hbox0 = new HBox(true, 1);
            hbox1 = new HBox(true, 1);
            hbox2 = new HBox(true, 1);
            hbox3 = new HBox(true, 1);
            vbox = new VBox(true, 1);


            Label label = new Label("Name");
            Entry entry = new Entry();
            

            Label label2 = new Label("Decryption Key");
            Entry decryptionKeyEntry = new Entry();

            Label label3 = new Label("Port");
            Entry portEntry = new Entry();

            entry.Text = Connection.username ;
            decryptionKeyEntry.Text = Connection.decryptionCode.ToString() ;
            portEntry.Text = Connection.port.ToString() ;



            Button confirmButton = new Button("Confirm");
            Button cancelButton = new Button("Cancel");

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


            vbox.PackStart(hbox0, false, false, 5);
            vbox.PackStart(hbox1, false, false, 5);
            vbox.PackStart(hbox2, false, false, 5);
            vbox.PackEnd(hbox3, false, false, 5);

            confirmButton.Clicked += delegate
            {

                Connection.username = entry.Text;
                try
                {
                    Connection.decryptionCode = Int32.Parse(decryptionKeyEntry.Text);
                    Connection.port = Int32.Parse(portEntry.Text);
                }
                catch (Exception ex)
                {
                    MessageDialog md = new MessageDialog(this,DialogFlags.DestroyWithParent,MessageType.Error,ButtonsType.Close, "input error");
                    md.Run();
                    md.Destroy();
                }
                MessageDialog mdo = new MessageDialog(this, 
                DialogFlags.DestroyWithParent, MessageType.Info, 
                ButtonsType.Close, "input save");
            mdo.Run();
            mdo.Destroy();

                Hide();

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