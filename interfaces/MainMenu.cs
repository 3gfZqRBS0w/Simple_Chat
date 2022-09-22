using System;
using Gtk;
using System.Collections;
using SimpleChat.communications;
using SimpleChat.interfaces;
using System.Collections.Generic;
using Cairo;

namespace SimpleChat.interfaces
{
    public class Simple_Chat : Window
    {

        const string TITLEOFAPPLICATION = "Simple Chat";
        public int sizeOfApplicationX = 500, sizeOfApplicationY = 550;

        private Entry entry;

        private List<Message> listOfMessage = new List<Message>();
     
        
        private ListStore store;
        private TreeView treeView ;
        private Statusbar statusbar;
        private ScrolledWindow sw ;


        private VBox vbox ;
        private HBox hbox ;

        private enum Column
        {
            Date,
            Name,
            Content

        }



        public Simple_Chat() : base(TITLEOFAPPLICATION)
        {
            listOfMessage.Add(new Message("You need to configure the decryption key and port", DateTime.Now,"Program", System.Net.IPAddress.Parse("127.0.0.1"))) ;


            SetDefaultSize(sizeOfApplicationX, sizeOfApplicationY);
            SetPosition(WindowPosition.Center);
            
            BorderWidth = 8;
            DeleteEvent += delegate { Application.Quit(); };

            Toolbar toolBar = new Toolbar();
            toolBar.ToolbarStyle = ToolbarStyle.Icons;

            ToolButton newConnection = new ToolButton(Stock.Network);

            ToolButton saveExchange = new ToolButton(Stock.Open);

            ToolButton quit = new ToolButton(Stock.Quit);

            ToolButton about = new ToolButton(Stock.About);

            entry = new Entry();

            Button send = new Button("Send");

            sw = new ScrolledWindow();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);

            store = CreateModel();

            treeView = new TreeView(store);
            treeView.RulesHint = true;
            treeView.RowActivated += OnRowActivated;
            
           sw.Add(treeView);
            

            AddColumns(treeView);

            statusbar = new Statusbar();

            toolBar.Insert(newConnection, 0);
            toolBar.Insert(saveExchange, 2);
            toolBar.Insert(quit, 3);
            toolBar.Insert(about, 4);

            quit.Clicked += exitApplication;
            about.Clicked += About;
            send.Clicked += OnSendMessage;
            saveExchange.Clicked += delegate { 
                new OpenSave();
            } ;
            newConnection.Clicked += delegate {
                new ConnectionMenu();
        };

            vbox = new VBox(false, 2);
            hbox = new HBox(false, 2);

            vbox.PackStart(toolBar, false, false, 0);
            vbox.PackStart(sw, true, true, 5);
            vbox.PackEnd(hbox, false, false, 0);

            hbox.Add(entry);
            hbox.Add(send);

            Add(vbox);

            ShowAll();
        }

        private void About(object sender, EventArgs args)
        {
            AboutDialog about = new AboutDialog();
            about.ProgramName = "Simple Chat";
            about.Version = "0.1";
            about.Copyright = "Lombres";
            about.Comments = @"Simple text is a simple tool for 
transmit texts";


            about.Run();
            about.Destroy();
        }

        private ListStore CreateModel()
        {
            ListStore store = new ListStore(typeof(string),typeof(string), typeof(string));

            foreach (Message message in listOfMessage)
            {
                store.AppendValues(message.getDate(), message.getSenderName(), message.getContent());
            }


            return store;
        }


        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Name", rendererText,
                "text", Column.Name);
            column.SortColumnId = (int)Column.Name;
            treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Date", rendererText,
                "text", Column.Date);
            column.SortColumnId = (int)Column.Date;
            treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Message", rendererText,
                "text", Column.Content);
            column.SortColumnId = (int)Column.Content;
            treeView.AppendColumn(column);
        }

            void OnRowActivated (object sender, RowActivatedArgs args) {

        TreeIter iter;        
        TreeView view = (TreeView) sender;   
        
        if (view.Model.GetIter(out iter, args.Path)) {
            string row = (string) view.Model.GetValue(iter, (int) Column.Name );
            row += ", " + (string) view.Model.GetValue(iter, (int) Column.Date );
            row += ", " + view.Model.GetValue(iter, (int) Column.Content );
            statusbar.Push(0, row);
        }
    }

    public void AddMessage(Message message) {
        store.AppendValues(message.getDate(), message.getSenderName(), message.getContent());
        listOfMessage.Add(message);

    }


    private void ShowSaveMenu(object sender, EventArgs e) {
        
    }




        private void OnSendMessage(object sender, EventArgs args)
        {
            Console.WriteLine("Sending Message") ;

            Message message = new Message(entry.Text, DateTime.Now,Connection.username, System.Net.IPAddress.Parse("127.0.0.1")) ;

            Connection.SendPacket(message) ;

            listOfMessage.Add(message) ;


           /*
           To append value in table*/
            store.AppendValues(message.getDate(), message.getSenderName(), message.getContent());
          
        }

    


        private void exitApplication(object sender, EventArgs args)
        {
            Environment.Exit(0);
        }


    }
}