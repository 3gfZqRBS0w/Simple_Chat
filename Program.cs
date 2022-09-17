using Gtk;
 
class SimpleChat : Window {
 
    public SimpleChat() : base("Simple Chat")
    {
        SetDefaultSize(250, 200);
        SetPosition(WindowPosition.Center);
        
        DeleteEvent += delegate { Application.Quit(); };
        
        Show();    
    }
    
    public static void Main()
    {
        Application.Init();
        new SimpleChat();        
        Application.Run();
    }
}