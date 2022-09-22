using System;
using Gtk;

// Work in progress
namespace SimpleChat.interfaces {
    public class OpenSave : Window {
        public OpenSave() : base("Select your path") {
            SetDefaultSize(400, 200);
            SetPosition(WindowPosition.Center); 

            BorderWidth = 20 ; 

        }

    }
}