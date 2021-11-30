using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AssemblyBrowser;
using Microsoft.Win32;

namespace Assembly_Viever
{
    public class AssemblyViewModel : INotifyPropertyChanged
    {
        public AssemblyViever Assembly { get; private set; }
        public List<NamespaceDescription> namespaces { get; private set; }

        private MyCommand openCommand;
        //паттерн Mvmv
        public MyCommand OpenCommand
        {
            get
            {
                return openCommand ??
                       (openCommand = new MyCommand(obj =>
                       {
                           try
                           {
                               OpenFileDialog openFileDialog = new OpenFileDialog();
                               openFileDialog.Filter = "Dynamic Link Library|*.DLL";
                               if (openFileDialog.ShowDialog() == true)
                               {

                                   this.Assembly.VievAssembly(openFileDialog.FileName);
                                   this.namespaces = Assembly.namespaces;
                                   OnPropertyChanged(nameof(namespaces));
                                   
                               }
                           }
                           catch (Exception e)
                           {
                               MessageBox.Show("Cannot load assembly");
                           }
                       }));
            }
        }
        private MyCommand closeCommand;
        public MyCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                       (closeCommand = new MyCommand(obj =>
                       {
                           try
                           {
                               Assembly.CloseAssembly();
                               this.namespaces = Assembly.namespaces;
                               OnPropertyChanged(nameof(namespaces));
                               OnPropertyChanged(nameof(Assembly));
                           }
                           catch (Exception e)
                           {
                               MessageBox.Show("Cannot close assembly");
                           }
                       }));
            }
        }


        public AssemblyViewModel()
        {
            Assembly = new AssemblyViever();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
